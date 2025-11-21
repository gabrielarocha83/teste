ALTER Procedure [dbo].[spBuscaGrupoEconomico]
(
	@pClienteId AS UNIQUEIDENTIFIER,
	@pEmpresaID varchar(10)
)
AS
BEGIN

		Declare @HojeSemHora datetime

		Set @HojeSemHora = DATEADD(dd, DATEDIFF(dd, 0, getdate()), 0)

	   SELECT
			ge.ID AS 'GrupoId',
			ge.Nome AS 'GrupoNome',
			ge.CodigoGrupo AS 'GrupoCodigo',
			ISNULL(ge.DataAlteracao, ge.DataCriacao) as 'UltimaAtualizacao',
			IIF(trge.ClassificacaoGrupoEconomicoID = 1, ISNULL(sub2.LCGrupo,0), isnull(sub.LcGrupo,0)) AS 'LcTotalGrupo',
			trge.Nome as 'TipoRelacao',
			IIF(trge.ClassificacaoGrupoEconomicoID = 1, ISNULL(sub2.ExpGrupo,0), isnull(sub.ExpGrupo,0)) AS 'ExpTotalGrupo',
			cge.Nome AS 'ClassificacaoNome',
			cge.ID AS 'ClassificacaoID',
			ge.StatusGrupoEconomicoFluxoID+' - '+sgef.Nome AS 'StatusGrupo',
			gem.ExplodeGrupo AS 'ExplodeGrupo'
		FROM dbo.GrupoEconomico ge
		INNER JOIN TipoRelacaoGrupoEconomico trge on trge.ID = ge.TipoRelacaoGrupoEconomicoID
		INNER JOIN dbo.GrupoEconomicoMembro gem ON gem.GrupoEconomicoID = ge.ID
		INNER JOIN dbo.ContaCliente cc ON cc.ID = gem.ContaClienteID
		INNER JOIN dbo.StatusGrupoEconomicoFluxo sgef ON sgef.ID = ge.StatusGrupoEconomicoFluxoID
		INNER JOIN dbo.ClassificacaoGrupoEconomico cge ON cge.ID = ge.ClassificacaoGrupoEconomicoID
		LEFT JOIN
		(
			SELECT
				gem1.GrupoEconomicoID,
				ISNULL(SUM(Case When CCF.VigenciaFim >= @HojeSemHora Then ccf.LC Else 0 End), 0) AS LcGrupo,
				ISNULL(SUM(ccf.Exposicao), 0) AS ExpGrupo
			FROM GrupoEconomicoMembro gem1
			INNER JOIN ContaClienteFinanceiro ccf ON ccf.ContaClienteId = gem1.ContaClienteId
			WHERE ccf.EmpresasID = @pEmpresaID	
			AND gem1.Ativo = 1												
			GROUP BY gem1.GrupoEconomicoID
		) AS sub ON sub.GrupoEconomicoID = gem.GrupoEconomicoID and trge.ClassificacaoGrupoEconomicoID > 1
		LEFT JOIN
		(
			SELECT
				gem1.GrupoEconomicoID,
				ISNULL(Max(Case When CCF.VigenciaFim >= @HojeSemHora Then ccf.LC Else 0 End), 0) AS LcGrupo,
				ISNULL(max(ccf.Exposicao), 0) AS ExpGrupo
			FROM GrupoEconomicoMembro gem1
			INNER JOIN ContaClienteFinanceiro ccf ON ccf.ContaClienteId = gem1.ContaClienteId
			WHERE ccf.EmpresasID = @pEmpresaID		
			AND gem1.MembroPrincipal = 1
			AND gem1.Ativo = 1																			
			GROUP BY gem1.GrupoEconomicoID
		) AS sub2 ON sub2.GrupoEconomicoID = gem.GrupoEconomicoID and trge.ClassificacaoGrupoEconomicoID = 1
	   WHERE cc.id = @pClienteId AND gem.Ativo = 1 AND ge.Ativo = 1 AND ge.EmpresasID = @pEmpresaID	;
	END;