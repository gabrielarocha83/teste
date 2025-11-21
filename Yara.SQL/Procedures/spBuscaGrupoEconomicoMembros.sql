ALTER Procedure [dbo].[spBuscaGrupoEconomicoMembros]
(
	@pGrupoId AS  UNIQUEIDENTIFIER,
	@EmpresaID CHAR(1)
)
AS
BEGIN
	SELECT DISTINCT
		ge.ID AS 'GrupoId', 
		ge.Nome AS 'GrupoNome', 
		cc.ID AS 'ClienteId', 
		cc.Nome AS 'ClienteNome', 
		cc.CodigoPrincipal AS 'ClienteCodigo', 
		cc.Documento AS 'ClienteDocumento', 
		cc.TipoClienteID AS 'ClienteTipoClienteID', 
		tc.TipoSerasa AS 'ClienteTipoSerasa',
		Case When ge.ClassificacaoGrupoEconomicoID = 1 Then 0 Else Isnull(ccf.LC, 0) End AS 'LcIndividual',
		Case When ge.ClassificacaoGrupoEconomicoID = 1 Then dbo.ExposicaoContaCliente(cc.ID,@EmpresaID) Else IsNull(CCF.Exposicao,0) End AS 'ExpIndividual',
		cge.Nome AS 'ClassificacaoNome', 
		ge.StatusGrupoEconomicoFluxoID+' - '+sgef.Nome AS 'StatusGrupo', 
		gem.StatusGrupoEconomicoFluxoID+' - '+sgef2.Nome AS 'StatusMembro', 
		gem.MembroPrincipal, 
		gem.ExplodeGrupo,
		IIF(CCPG.ID IS NULL, CAST(0 AS BIT), CAST(1 AS BIT)) AS 'PossuiGarantia', 
		cc.RestricaoSerasa, 
		cc.PendenciaSerasa, 
		cc.BloqueioManual
	FROM GrupoEconomico ge
	INNER JOIN GrupoEconomicoMembro gem
	ON gem.GrupoEconomicoID = ge.ID
	INNER JOIN ContaCliente cc
	ON cc.ID = gem.ContaClienteID
	INNER JOIN TipoCliente tc
	ON tc.ID = cc.TipoClienteID
	LEFT JOIN ContaClienteFinanceiro ccf
	ON ccf.ContaClienteID = cc.ID AND ccf.EmpresasID = @EmpresaID
	INNER JOIN StatusGrupoEconomicoFluxo sgef
	ON sgef.ID = ge.StatusGrupoEconomicoFluxoID
	INNER JOIN StatusGrupoEconomicoFluxo sgef2
	ON sgef2.ID = gem.StatusGrupoEconomicoFluxoID
	INNER JOIN ClassificacaoGrupoEconomico cge
	ON cge.ID = ge.ClassificacaoGrupoEconomicoID
	LEFT JOIN ContaClienteParticipanteGarantia CCPG 
	ON (CCPG.Documento = cc.Documento And CCPG.Garantido = 1 And Exists (Select 1 From ContaClienteGarantia CCGX Where CCGX.ID = CCPG.ContaClienteGarantiaID And CCGX.Ativo = 1 And CCGX.EmpresasID = @EmpresaID))
	WHERE
		ge.EmpresasID = @EmpresaID AND
		ge.ID = @pGrupoId AND
		ge.Ativo = 1 AND
		gem.Ativo = 1
END