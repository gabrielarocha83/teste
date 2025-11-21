ALTER Procedure [dbo].[spBuscaContaCliente]
(
	@pNome VARCHAR(120) = NULL,
	@pDocumento VARCHAR(120) = NULL,
	@pCodigo VARCHAR(10) = NULL,
	@pApelido VARCHAR(120) = NULL,
	@pGrupo VARCHAR(120) = NULL,
	@pEmpresaId VARCHAR(1) = NULL,
	@pNumeroOrdem VARCHAR(10) = NULL,
	@pUsuarioId UNIQUEIDENTIFIER = NULL
)
AS
BEGIN
	Declare @pOrdensExposicao AS VARCHAR(MAX);
	Declare @pCCID UNIQUEIDENTIFIER = null
	Declare @FiltrarClientes BIT = 0
	-- Set @pCCID = null
	-- Set @FiltrarClientes = 0

	Create Table #AcessoClientes (ContaClienteID uniqueidentifier)

	If @pUsuarioId IS NOT NULL
	Begin

		Declare @IdPerfil uniqueidentifier

		Select Top 1 @IdPerfil = P.Id
		From EstruturaPerfilUsuario EPUL
		Inner Join Perfil P On (P.ID = EPUL.PerfilId)
		Where EPUL.UsuarioId=@pUsuarioId
		Order By P.Ordem Desc

		-- CTCs
		If @IdPerfil = '5F552012-6956-49C8-8A5B-61436BF75A3E' -- Id do Perfil CTC
		Begin

			Set @FiltrarClientes = 1
			
			Insert Into #AcessoClientes
			Select Distinct CCE.ContaClienteId
			From EstruturaPerfilUsuario EPU
			Inner Join EstruturaComercial EC On (EC.EstruturaComercialPapelID = 'C' And EC.CodigoSap = EPU.CodigoSap)
			Inner Join ContaCliente_EstruturaComercial CCE On (CCE.EstruturaComercialId = EC.CodigoSap)
			Where EPU.UsuarioId = @pUsuarioId
				AND EPU.PerfilId = @IdPerfil -- Id do Perfil CTC
				AND CCE.Ativo = 1

		End

		-- Representantes
		If Exists(Select 1 From Usuario_Representante URX Where URX.UsuarioID=@pUsuarioId)
		Begin

			Set @FiltrarClientes = 1
			
			Insert Into #AcessoClientes
			Select Distinct CCR.ContaClienteID
			From Usuario_Representante UR
			Inner Join ContaCliente_Representante CCR ON (CCR.RepresentanteID = UR.RepresentanteID)
			Where UR.UsuarioId = @pUsuarioId
				AND CCR.Ativo = 1

		End

	End

	If @pNumeroOrdem IS NOT NULL
	Begin

		Declare @ov bigint

		Set @ov = Try_Cast(@pNumeroOrdem as bigint)

		If @ov is not null
		Begin

			SELECT @pOrdensExposicao = ps.Valor FROM dbo.ParametroSistema ps WHERE ps.Tipo = 'exposicao' AND ps.Chave = 'tiposordem' AND ps.EmpresasID = @pEmpresaId;

			SELECT @pCCID = CC.ID
			FROM dbo.ContaCliente cc
			INNER JOIN dbo.ContaClienteCodigo ccc ON ccc.ContaClienteID = cc.ID
			INNER JOIN dbo.OrdemVenda ov ON ov.Pagador = ccc.Codigo
			INNER JOIN dbo.ItemOrdemVenda iov ON iov.OrdemVendaNumero = ov.Numero
			INNER JOIN dbo.DivisaoRemessa dr ON dr.ItemOrdemVendaItem = iov.Item AND dr.OrdemVendaNumero = iov.OrdemVendaNumero
			WHERE dr.QtdEntregue < dr.QtdPedida
			And Try_Cast(ov.Numero as bigint) = @ov
			AND (
			(@pEmpresaId = 'Y' And ov.OrgVendas in ('YBR1','YBR2','YBR3')) OR
			(@pEmpresaId = 'G' And ov.OrgVendas in ('YGA1','YGA2','YGA3'))
			)
			AND (ov.Tipo in (Select splitdata from fnSplitString(@pOrdensExposicao, ',')))
			And (RTrim(IsNull(IOV.MotivoRecusa,'')) = '')
			ORDER BY cc.ID, ccc.Codigo;

		End

	End

    DECLARE @sqlComand NVARCHAR(MAX) = N'
	SELECT cc.ID, 
			cc.codigoprincipal, 
			cc.nome, 
			cc.documento, 
			cc.apelido, 
			cc.Simplificado,
			min(ge.Nome) AS GrupoEconomico
		FROM dbo.ContaCliente cc
		LEFT JOIN dbo.GrupoEconomicoMembro gem ON gem.contaclienteid = cc.id  And GEM.Ativo = 1
		LEFT JOIN dbo.GrupoEconomico ge ON ge.id = gem.grupoeconomicoid AND ge.EmpresasID = '''+@pEmpresaId+''' And GE.Ativo = 1
	WHERE 1 = 1';

    IF (@pNome IS NOT NULL)
    BEGIN
        SET @sqlComand+=N' AND UPPER(cc.nome) LIKE UPPER(''%'+@pNome+'%'') COLLATE Latin1_General_CI_AI';
    END;

    IF (@pDocumento IS NOT NULL)
    BEGIN
        SET @sqlComand+=N' AND EXISTS(SELECT CCC.* FROM ContaClienteCodigo CCC WHERE CCC.Documento LIKE ''%'+@pDocumento+'%'' AND CCC.ContaClienteID = cc.ID)';
    END;

    IF (@pCodigo IS NOT NULL)
    BEGIN
		IF ISNUMERIC(LTrim(RTrim(@pCodigo))) = 1
		BEGIN
			SET @pCodigo = Right('0000000000'+LTrim(RTrim(@pCodigo)),10);
			SET @sqlComand+=N' AND EXISTS(SELECT CCC.* FROM ContaClienteCodigo CCC WHERE CCC.ContaClienteID = cc.ID AND CCC.Codigo='''+@pCodigo+''')';
		END
		ELSE
		BEGIN
			SET @pCodigo = LTrim(RTrim(@pCodigo));
			SET @sqlComand+=N' AND EXISTS(SELECT CCC.* FROM ContaClienteCodigo CCC WHERE CCC.ContaClienteID = cc.ID AND CCC.Codigo LIKE UPPER(''%'+@pCodigo+'%'') COLLATE Latin1_General_CI_AI)';
		END
    END;

    IF (@pApelido IS NOT NULL)
    BEGIN
        SET @sqlComand+=N' AND UPPER(cc.Apelido) LIKE UPPER(''%'+@pApelido+'%'') COLLATE Latin1_General_CI_AI';
    END;

    IF (@pGrupo IS NOT NULL)
    BEGIN
        SET @sqlComand+=N' AND UPPER(ge.Nome) LIKE UPPER(''%'+@pGrupo+'%'') COLLATE Latin1_General_CI_AI';
    END;

    IF (@pNumeroOrdem IS NOT NULL)
    BEGIN
        SET @sqlComand+=N' and cc.ID = '''+convert(varchar(100),@pCCID)+''''
    END;

	IF (@FiltrarClientes = 1)
	BEGIN
		SET @sqlComand+=N' AND cc.ID in (Select Distinct ContaClienteId From #AcessoClientes)'
	END;

    SET @sqlComand+=N' Group By cc.ID, cc.codigoprincipal, cc.nome, cc.documento, cc.apelido, cc.Simplificado';
     
	EXECUTE sp_executesql
            @statement = @sqlComand;

	DROP TABLE #AcessoClientes

END
