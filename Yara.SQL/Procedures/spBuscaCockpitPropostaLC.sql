ALTER Procedure [dbo].[spBuscaCockpitPropostaLC]
	@Usuario   UNIQUEIDENTIFIER,
	@EmpresaID CHAR(1)
AS
BEGIN

	DECLARE @pValorAVista VARCHAR(60)
	DECLARE @pOrdensExposicao VARCHAR(60)

	SELECT @pValorAVista = ps.Valor FROM ParametroSistema ps WHERE ps.Tipo = 'ordem' AND ps.Chave = 'condicaovista' AND ps.EmpresasID = @EmpresaID;         
	SELECT @pOrdensExposicao = ps.Valor FROM ParametroSistema ps WHERE ps.Tipo = 'exposicao' AND ps.Chave = 'tiposordem' AND ps.EmpresasID = @EmpresaID;

	SELECT DISTINCT
		P.ID,
		CC.ID AS ContaClienteID, 
		IIF(P.Ecomm = 1, 'EC', 'LC') + RIGHT(REPLICATE('0',5) + CONVERT(VARCHAR(MAX),P.NumeroInternoProposta),5) +'/' + CONVERT(VARCHAR,YEAR(P.DataCriacao)) AS Codigo, 
		CC.Nome AS Nome, 
		C.Nome AS CTC, 
		G.Nome AS GC, 
		D.Nome AS DR, 
		R.Nome AS Representante,
		S.Nome AS [Status], 
		IsNull(P.LCProposto, 0) AS Valor, 
		LTPLC.LeadTime, 
		U.Nome AS Responsavel, 
        ISNULL(CCF.LC, 0) as LcAtual, 
        CCF.VigenciaFim as VigenciaLc 
	INTO #P
	FROM PropostaLC P 
	INNER JOIN PropostaLCStatus S ON S.ID = P.PropostaLCStatusID 
	INNER JOIN ContaCliente CC ON CC.ID = P.ContaClienteID 
	LEFT JOIN ContaClienteCodigo ccc ON ccc.ContaClienteID = cc.ID
	LEFT JOIN Representante R ON R.ID = P.RepresentanteID
	LEFT JOIN EstruturaComercial C ON C.CodigoSap = P.CodigoSap 
	LEFT JOIN EstruturaComercial G ON G.CodigoSap = C.Superior_CodigoSap 
	LEFT JOIN EstruturaComercial D ON D.CodigoSap = G.Superior_CodigoSap 
	LEFT JOIN Usuario U ON U.ID = P.ResponsavelID 
	LEFT JOIN ContaClienteFinanceiro CCF ON (CCF.ContaClienteID = P.ContaClienteID AND CCF.EmpresasID = P.EmpresaID)
	CROSS APPLY ( SELECT LeadTime FROM dbo.fnLeadTimePropostaLC(P.ID, P.PropostaLCStatusID) ) LTPLC
	WHERE
		P.ResponsavelID = @Usuario 
		AND P.EmpresaID = @EmpresaID AND PropostaLCStatusID NOT IN('AA','XE','XR')

	SELECT
		P.ContaClienteID,
		MIN(dr.DataRemessa) AS Data
	INTO #R
    FROM #P P
    INNER JOIN ContaClienteCodigo ccc ON ccc.ContaClienteID = P.ContaClienteID
    INNER JOIN OrdemVenda ov ON ov.Pagador = ccc.Codigo
    INNER JOIN ItemOrdemVenda iov ON iov.OrdemVendaNumero = ov.Numero
    INNER JOIN DivisaoRemessa dr ON dr.ItemOrdemVendaItem = iov.Item AND dr.OrdemVendaNumero = iov.OrdemVendaNumero
    LEFT JOIN StatusOrdemVendas sov ON sov.[Status] = dr.BloqueioPortal
    WHERE dr.QtdEntregue < dr.QtdPedida
        AND
		(
			(@EmpresaID = 'Y' And ov.OrgVendas in ('YBR1','YBR2','YBR3')) OR
			(@EmpresaID = 'G' And ov.OrgVendas in ('YGA1','YGA2','YGA3','1000'))
        )
		AND (ov.Tipo in (SELECT splitdata FROM fnSplitString(@pOrdensExposicao, ',')))
		AND (CASE WHEN RTRIM(IOV.CondPagto) = '' THEN OV.CondPagto ELSE IOV.CondPagto END NOT IN (SELECT splitdata FROM fnSplitString(@pValorAVista, ',')))
		AND iov.PagaRetira = 0
		AND (RTrim(IsNull(IOV.MotivoRecusa,'')) = '')
	GROUP BY P.ContaClienteID

	SELECT
		P.*,
		ISNULL(R.[Data],'2970-01-01') AS [Data]
	FROM #P P
	LEFT JOIN #R R ON R.ContaClienteID = P.ContaClienteID

	DROP TABLE #P
	DROP TABLE #R

END