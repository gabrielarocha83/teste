ALTER PROCEDURE [dbo].[spBuscaClientePropostaRenovacaoLC]
(
	@pContaClienteID uniqueidentifier = NULL,
	@pEmpresasID char(1) = NULL,
	@pCodigo varchar(10) = NULL,
	@pNome nvarchar(max) = NULL,
	@pApelido nvarchar(max) = NULL,
	@pDocumento nvarchar(max) = NULL,
	@pSegmentacao nvarchar(max) = NULL,
	@pCategorizacao nvarchar(max) = NULL,
	@pNomeGrupo nvarchar(max) = NULL,
	@pVigenciaInicial datetime = NULL,
	@pVigenciaFinal datetime = NULL,
	@pRating nvarchar(max) = NULL,
	@pValorLCInicial decimal(18,2) = NULL,
	@pValorLCFinal decimal(18,2) = NULL,
	@pClienteTop10Sim bit = NULL,
	@pClienteTop10Nao bit = NULL,
	@pConsultaSerasaInicial datetime = NULL,
	@pConsultaSerasaFinal datetime = NULL,
	@pRestricaoSerasa int = NULL,
	@pRestricoesYaraSim bit = NULL,
	@pRestricoesYaraNao bit = NULL,
	@pRestricoesSerasaGrupoSim bit = NULL,
	@pRestricoesSerasaGrupoNao bit = NULL,
	@pRestricoesYaraGrupoSim bit = NULL,
	@pRestricoesYaraGrupoNao bit = NULL,
	@pLCAndamentoSim bit = NULL,
	@pLCAndamentoNao bit = NULL,
	@pAlcadaAndamentoSim bit = NULL,
	@pAlcadaAndamentoNao bit = NULL,
	@pComComprasInicial datetime = NULL,
	@pComComprasFinal datetime = NULL,
	@pVigenciaGarantiaInicial datetime = NULL,
	@pVigenciaGarantiaFinal datetime = NULL,
	@pRepresentanteID uniqueidentifier = NULL,
	@pAnalistaID uniqueidentifier = NULL,
	@pCTC nvarchar(max) = NULL,
	@pGC nvarchar(max) = NULL,
	@pDiretoria nvarchar(max) = NULL,
	@pPropostaRenovacaoInicial datetime = NULL,
	@pPropostaRenovacaoFinal datetime = NULL,
	@pClientesRenovadosSim bit = NULL,
	@pClientesRenovadosNao bit = NULL,
	@pXMLGuidList xml = NULL
)
AS
BEGIN

	-- Variáveis
	Declare @PerfilAnalistaID uniqueidentifier = NULL

	-- Tabelas
	Declare @ContaCliente_TMP table (ContaClienteID uniqueidentifier)

	-- Seleciona o ID do Perfil de Analista de Crédito
	Select @PerfilAnalistaID = ID From Perfil Where Descricao Like '%Analista de Crédito%'

	-- Seleciona os IDs das contas clientes que vieram do XML
	Insert Into @ContaCliente_TMP (ContaClienteID)
	Select CONVERT(uniqueidentifier, t.p.value('(./value)[1]', 'varchar(36)')) AS ContaClienteID From @pXMLGuidList.nodes('/guid') t(p)
	
	-- Se existir código, ajusta no formato correto (10 dígitos)
	If (@pCodigo Is Not Null)
	Begin
		Set @pCodigo = Right('0000000000'+LTrim(RTrim(@pCodigo)),10)
	End

	-- Dados Cliente
	Select CC.ID AS ContaClienteID
	Into #ContaCliente
	From ContaCliente CC
	Where
		(@pContaClienteID is null Or CC.ID = @pContaClienteID) And
		(@pXMLGuidList is null Or CC.ID IN (Select ContaClienteID From @ContaCliente_TMP)) And
		(@pNome is null Or CC.Nome like '%' + @pNome + '%') And
		(@pApelido is null Or CC.Apelido like '%' + @pApelido + '%') And
		(@pCodigo is null Or Exists (Select 1 From ContaClienteCodigo CCC Where CCC.ContaClienteID = CC.ID And CCC.Codigo = @pCodigo)) And
		(@pDocumento is null Or Exists (Select 1 From ContaClienteCodigo CCC Where CCC.ContaClienteID = CC.ID And CCC.Documento = @pDocumento)) And
		(@pSegmentacao is null Or CC.Segmentacao like '%' + @pSegmentacao + '%') And
		(@pCategorizacao is null Or CC.Categorizacao like '%' + @pCategorizacao + '%')
		
	-- Dados Grupo
	Select CC.ContaClienteID, GE.ID AS GrupoEconomicoID, (Select Top 1 ContaClienteID From GrupoEconomicoMembro GEMX Where GEMX.Ativo = 1 And GEMX.GrupoEconomicoID = GE.ID And GEMX.MembroPrincipal = 1) AS MembroPrincipalID, GE.Nome AS NomeGrupo, CGE.Nome AS ClassificacaoGrupo, CGE.ID As ClassificacaoGrupoID
	Into #GrupoEconomico
	From GrupoEconomicoMembro GEM
	Inner Join GrupoEconomico GE On (GE.ID = GEM.GrupoEconomicoID And GE.Ativo = 1 And GE.EmpresasID = @pEmpresasID)
	Inner Join ClassificacaoGrupoEconomico CGE On (CGE.ID = GE.ClassificacaoGrupoEconomicoID)
	Inner Join #ContaCliente CC On (CC.ContaClienteID = GEM.ContaClienteID)
	Where
		GE.EmpresasID = @pEmpresasID And
		GEM.Ativo = 1

	-- Conta Credito
	Select
		IIf(GE.MembroPrincipalID Is Null Or GE.ClassificacaoGrupoID = 2, CC.ContaClienteID, GE.MembroPrincipalID) AS ContaClienteID,
		CCX.Nome AS NomeCliente,
		CCX.CodigoPrincipal AS CodigoCliente,
		CCX.Apelido AS Apelido,
		CCX.Documento AS Documento,
		CCX.Segmentacao As Segmentacao,
		CCX.Categorizacao As Categorizacao,
		CCX.TOP10 AS Top10,
		CCX.PendenciaSerasa AS RestricaoSerasa,
		CCX.RestricaoSerasa AS RestricaoSerasaFlag,
		CCX.BloqueioManual AS BloqueioManualFlag,
		GE.NomeGrupo,
		GE.ClassificacaoGrupo,
		TC.Nome AS TipoCliente
	Into #ContaCredito
	From #ContaCliente CC
	Left Join #GrupoEconomico GE On (GE.ContaClienteID = CC.ContaClienteID)
	Inner Join ContaCliente CCX On (CCX.ID = IIf(GE.MembroPrincipalID Is Null Or GE.ClassificacaoGrupoID = 2, CC.ContaClienteID, GE.MembroPrincipalID))
	Left Join TipoCliente TC On (TC.ID = CCX.TipoClienteID)
	Where
		(@pNomeGrupo is null Or GE.NomeGrupo like '%' + @pNomeGrupo + '%') And
		((@pClienteTop10Sim is null And @pClienteTop10Nao is null) Or (CCX.Top10 = 1 And @pClienteTop10Sim = 1) Or (CCX.Top10 = 0 And @pClienteTop10Nao = 1)) And
		(@pRestricaoSerasa is null Or CCX.PendenciaSerasa = @pRestricaoSerasa)
	Group By IIf(GE.MembroPrincipalID Is Null Or GE.ClassificacaoGrupoID = 2, CC.ContaClienteID, GE.MembroPrincipalID), CCX.Nome, CCX.CodigoPrincipal, CCX.Apelido, CCX.Documento, CCX.Segmentacao, CCX.Categorizacao, CCX.TOP10, CCX.PendenciaSerasa, CCX.RestricaoSerasa, CCX.BloqueioManual, GE.NomeGrupo, GE.ClassificacaoGrupo, TC.Nome

	-- Dados Financeiros (Conta Credito)
	Select CX.*, CCF.VigenciaFim AS DataVigenciaLC, CCF.Rating AS Rating, CCF.LC AS ValorLC, CCF.DividaAtiva, CCF.ConceitoCobrancaID, CCF.GrupoEconomicoRestricao
	Into #Financeiro
	From #ContaCredito CX
	Inner Join ContaClienteFinanceiro CCF On (CCF.ContaClienteID = CX.ContaClienteID And CCF.EmpresasID = @pEmpresasID And CCF.LC > 0)
	Where
		(@pVigenciaInicial is null Or DateDiff(Day,@pVigenciaInicial,CCF.VigenciaFim) >= 0) And
		(@pVigenciaFinal is null Or DateDiff(Day,CCF.VigenciaFim,@pVigenciaFinal) >= 0) And
		(@pRating is null Or CCF.Rating like '%' + @pRating +'%') And
		(@pValorLCInicial is null Or CCF.LC >= @pValorLCInicial) And
		(@pValorLCFinal is null Or CCF.LC <= @pValorLCFinal) And
		((@pRestricoesYaraSim is null And @pRestricoesYaraNao is null) Or ((CX.RestricaoSerasaFlag = 1 Or CX.BloqueioManualFlag = 1 Or CCF.DividaAtiva = 1 Or CCF.ConceitoCobrancaID is not null) And @pRestricoesYaraSim = 1) Or (CX.RestricaoSerasaFlag = 0 And CX.BloqueioManualFlag = 0 And CCF.DividaAtiva = 0 And CCF.ConceitoCobrancaID is null And @pRestricoesYaraNao = 1)) And
		((@pRestricoesSerasaGrupoSim is null And @pRestricoesSerasaGrupoNao is null) Or (CCF.GrupoEconomicoRestricao = 1 And @pRestricoesSerasaGrupoSim = 1) Or (CCF.GrupoEconomicoRestricao = 0 And @pRestricoesSerasaGrupoNao = 1)) And
		((@pRestricoesYaraGrupoSim is null And @pRestricoesYaraGrupoNao is null) Or ((CCF.GrupoEconomicoRestricao = 1 Or CX.RestricaoSerasaFlag = 1 Or CX.BloqueioManualFlag = 1 Or CCF.DividaAtiva = 1 Or CCF.ConceitoCobrancaID is not null) And @pRestricoesYaraGrupoSim = 1) Or (CCF.GrupoEconomicoRestricao = 0 And CX.RestricaoSerasaFlag = 0 And CX.BloqueioManualFlag = 0 And CCF.DividaAtiva = 0 And CCF.ConceitoCobrancaID is null And @pRestricoesYaraGrupoNao = 1)) And
		((@pClientesRenovadosSim is null And @pClientesRenovadosNao is null) Or (@pClientesRenovadosSim = 1 And Exists (Select 1 From PropostaRenovacaoVigenciaLCCliente PRVCX Inner Join PropostaRenovacaoVigenciaLC PRVX On (PRVX.ID = PRVCX.PropostaRenovacaoVigenciaLCID) Where PRVCX.ContaClienteID = CX.ContaClienteID And PRVX.EmpresaID = @pEmpresasID And PRVX.PropostaLCStatusID = 'AA')) Or (@pClientesRenovadosNao = 1 And Not Exists (Select 1 From PropostaRenovacaoVigenciaLCCliente PRVCX Inner Join PropostaRenovacaoVigenciaLC PRVX On (PRVX.ID = PRVCX.PropostaRenovacaoVigenciaLCID) Where PRVCX.ContaClienteID = CX.ContaClienteID And PRVX.EmpresaID = @pEmpresasID And PRVX.PropostaLCStatusID = 'AA')))

	-- Estrutura Comercial
	Select CC.ContaClienteID, EC1.Nome AS CTC, EC2.Nome AS GC, EC3.Nome AS Diretoria, U.Nome AS Analista, U.ID As AnalistaID, EC1.CodigoSap As CodigoSapCTC, 
		(SELECT TOP 1 EstruturaComercialId FROM ContaCliente_EstruturaComercial WHERE ContaClienteID = CC.ContaClienteID AND EmpresasId = @pEmpresasID AND Ativo = 1) AS CodigoSapCTCTop1
	Into #EstruturaComercial
	From ContaCliente_EstruturaComercial CCE
	Inner Join #ContaCliente CC On (CC.ContaClienteID = CCE.ContaClienteId)
	Left Join EstruturaComercial EC1 On (EC1.CodigoSap = CCE.EstruturaComercialId And EC1.EstruturaComercialPapelID = 'C')
	Left Join EstruturaComercial EC2 On (EC2.CodigoSap = EC1.Superior_CodigoSap And EC2.EstruturaComercialPapelID = 'G')
	Left Join EstruturaComercial EC3 On (EC3.CodigoSap = EC2.Superior_CodigoSap And EC3.EstruturaComercialPapelID = 'D')
	Left Join EstruturaPerfilUsuario EPU On (EPU.CodigoSap = EC1.CodigoSap And EPU.PerfilId = @PerfilAnalistaID)
	Left Join Usuario U On (U.ID = EPU.UsuarioId)
	WHERE CCE.EmpresasId = @pEmpresasID
		AND CCE.Ativo = 1

	-- Representante
	Select CC.ContaClienteID, CCR.RepresentanteID, R.Nome, 
		(SELECT TOP 1 RepresentanteID FROM ContaCliente_Representante WHERE ContaClienteID = CC.ContaClienteID AND EmpresasID = @pEmpresasID AND Ativo = 1) AS RepresentateIDTop1
	Into #Representante
	From ContaCliente_Representante CCR
	Inner Join Representante R On (R.ID = CCR.RepresentanteID)
	Inner Join #ContaCliente CC On (CC.ContaClienteID = CCR.ContaClienteID)
	WHERE CCR.EmpresasID = @pEmpresasID
		AND CCR.Ativo = 1

	-- Compras
	Select F.ContaClienteID, Max(OV.DataEmissao) As DataUltimaCompra
	Into #Compras
	From OrdemVenda OV
	Inner Join ContaClienteCodigo CCC On (CCC.Codigo = OV.Pagador)
	Inner Join #Financeiro F On (F.ContaClienteID = CCC.ContaClienteID)
	Where
		(
			(@pEmpresasID = 'Y' And ov.OrgVendas in ('YBR1','YBR2','YBR3')) Or
			(@pEmpresasID = 'G' And ov.OrgVendas in ('YGA1','YGA2','YGA3'))
		)
	Group By F.ContaClienteID

	-- Garantias
	Select F.ContaClienteID, Max(CCG.VigenciaFim) as DataValidadeGarantia
	Into #Garantias
	From ContaClienteParticipanteGarantia CCPG
	Inner Join ContaClienteGarantia CCG On (CCG.ID = CCPG.ContaClienteGarantiaID)
	Inner Join #Financeiro F On (F.Documento = CCPG.Documento)
	Where
		CCG.EmpresasID = @pEmpresasID And 
		CCPG.Garantido = 1
	Group By F.ContaClienteID

	-- Serasa
	Select F.ContaClienteID, Max(SS.DataCriacao) as DataConsultaSerasa
	Into #Serasa
	From SolicitanteSerasa SS
	Inner Join #Financeiro F On (F.ContaClienteID = SS.ContaClienteID)
	Group By F.ContaClienteID

	-- PropostaLC (Em andamento)
	Select F.ContaClienteID, PLC.NumeroInternoProposta, PLC.DataCriacao, PLC.LCProposto as ValorPropostaLC, PLCS.Nome AS PropostaLCStatus, PLC.Ecomm
	Into #PropostaLC
	From PropostaLC PLC
	Inner Join PropostaLCStatus PLCS On (PLCS.ID = PLC.PropostaLCStatusID)
	Inner Join #Financeiro F On (F.ContaClienteID = PLC.ContaClienteID)
	Where
		PLC.EmpresaID = @pEmpresasID And
		PLC.PropostaLCStatusID NOT IN ('AA', 'XE', 'XR') And
		PLC.NumeroInternoProposta = (Select Top 1 NumeroInternoProposta From PropostaLC Where ContaClienteID = F.ContaClienteID Order By DataCriacao Desc)

	-- Proposta Alçada Comercial (Em andamento)
	Select F.ContaClienteID, PAC.NumeroInternoProposta, PAC.DataCriacao, PAC.LCProposto as ValorPropostaAC, PCS.Status As PropostaACStatus
	Into #PropostaAC
	From PropostaAlcadaComercial PAC
	Inner Join PropostaCobrancaStatus PCS On (PCS.ID = PAC.PropostaCobrancaStatusID)
	Inner Join #Financeiro F On (F.ContaClienteID = PAC.ContaClienteID)
	Where
		PAC.EmpresaID = @pEmpresasID And
		PAC.PropostaCobrancaStatusID NOT IN ('AA', 'AP', 'CA', 'EN', 'RE') And
		PAC.NumeroInternoProposta = (Select Top 1 NumeroInternoProposta From PropostaAlcadaComercial Where ContaClienteID = F.ContaClienteID Order By DataCriacao Desc)

	-- Proposta de Renovação de Vigencia de LC (Aprovada)
	Select PRVC.ContaClienteID, PRV.DataCriacao
	Into #PropostaRV
	From PropostaRenovacaoVigenciaLC PRV
	Inner Join PropostaRenovacaoVigenciaLCCliente PRVC On (PRVC.PropostaRenovacaoVigenciaLCID = PRV.ID)
	Where
		PRV.EmpresaID = @pEmpresasID And
		PRV.PropostaLCStatusID = 'AA'

	-- Resultado
	Select
		F.ContaClienteID,
		F.NomeCliente,
		F.CodigoCliente,
		F.Apelido,
		F.Documento,
		F.Segmentacao,
		F.Categorizacao,
		F.TipoCliente,
		F.NomeGrupo,
		F.ClassificacaoGrupo,
		F.DataVigenciaLC,
		F.Rating,
		F.ValorLC,
		F.Top10,
		S.DataConsultaSerasa,
		F.RestricaoSerasa,
		CONVERT(BIT, IIF((F.RestricaoSerasaFlag = 1 Or F.BloqueioManualFlag = 1 Or F.DividaAtiva = 1 Or F.ConceitoCobrancaID is not null), 1, 0)) AS RestricaoYara,
		CONVERT(BIT, IIF(F.GrupoEconomicoRestricao = 1, 1, 0)) AS RestricaoSerasaGrupo,
		CONVERT(BIT, IIF(F.NomeGrupo is not null And (F.GrupoEconomicoRestricao = 1 Or F.RestricaoSerasaFlag = 1 Or F.BloqueioManualFlag = 1 Or F.DividaAtiva = 1 Or F.ConceitoCobrancaID is not null), 1, 0)) AS RestricaoYaraGrupo,
		IIF(P.Ecomm = 1, 'EC', 'LC') + RIGHT(REPLICATE('0',5) + CONVERT(VARCHAR(MAX),P.NumeroInternoProposta),5) +'/' + CONVERT(VARCHAR,YEAR(P.DataCriacao)) AS CodigoPropostaLC, 
		P.ValorPropostaLC,
		P.PropostaLCStatus,
		'AC' + RIGHT(REPLICATE('0',5) + CONVERT(VARCHAR(MAX),PA.NumeroInternoProposta),5) +'/' + CONVERT(VARCHAR,YEAR(PA.DataCriacao)) AS CodigoPropostaAC, 
		PA.ValorPropostaAC,
		PA.PropostaACStatus,
		C.DataUltimaCompra AS DataUltimaCompra,
		G.DataValidadeGarantia,
		(Select R.Nome From #Representante R Where R.ContaClienteID = F.ContaClienteID And R.RepresentanteID = R.RepresentateIDTop1) As Representante,
		(Select EC.CTC From #EstruturaComercial EC Where EC.ContaClienteID = F.ContaClienteID And EC.CodigoSapCTC = EC.CodigoSapCTCTop1) As CTC,
		(Select EC.GC From #EstruturaComercial EC Where EC.ContaClienteID = F.ContaClienteID And EC.CodigoSapCTC = EC.CodigoSapCTCTop1) As GC,
		(Select EC.Diretoria From #EstruturaComercial EC Where EC.ContaClienteID = F.ContaClienteID And EC.CodigoSapCTC = EC.CodigoSapCTCTop1) As Diretoria,
		(Select EC.Analista From #EstruturaComercial EC Where EC.ContaClienteID = F.ContaClienteID And EC.CodigoSapCTC = EC.CodigoSapCTCTop1) As Analista
	From #Financeiro F
	Left Join #Compras C On (C.ContaClienteID = F.ContaClienteID)
	Left Join #Garantias G On (G.ContaClienteID = F.ContaClienteID)
	Left Join #Serasa S On (S.ContaClienteID = F.ContaClienteID)
	Left Join #PropostaLC P On (P.ContaClienteID = F.ContaClienteID)
	Left Join #PropostaAC PA On (PA.ContaClienteID = F.ContaClienteID)
	Left Join #PropostaRV PRV On (PRV.ContaClienteID = F.ContaClienteID)
	Where
		(@pCTC is null Or Exists (Select 1 From #EstruturaComercial Where ContaClienteID = F.ContaClienteID And CTC like '%' + @pCTC + '%')) And
		(@pGC is null Or Exists (Select 1 From #EstruturaComercial Where ContaClienteID = F.ContaClienteID And GC like '%' + @pGC + '%')) And
		(@pDiretoria is null Or Exists (Select 1 From #EstruturaComercial Where ContaClienteID = F.ContaClienteID And Diretoria like '%' + @pDiretoria + '%')) And
		(@pAnalistaID is null Or Exists (Select 1 From #EstruturaComercial Where ContaClienteID = F.ContaClienteID And AnalistaID = @pAnalistaID)) And
		(@pRepresentanteID is null Or Exists(Select 1 From #Representante Where ContaCLienteID = F.ContaClienteID And RepresentanteID = @pRepresentanteID)) And
		(@pComComprasInicial is null Or DateDiff(day,@pComComprasInicial,C.DataUltimaCompra) >= 0) And
		(@pComComprasFinal is null Or DateDiff(day,C.DataUltimaCompra,@pComComprasFinal) >= 0) And
		(@pVigenciaGarantiaInicial is null Or DateDiff(day,@pVigenciaGarantiaInicial,G.DataValidadeGarantia) >= 0) And
		(@pVigenciaGarantiaFinal is null Or DateDiff(day,@pVigenciaGarantiaFinal,G.DataValidadeGarantia) >= 0) And
		(@pConsultaSerasaInicial is null Or DateDiff(day,@pConsultaSerasaInicial,S.DataConsultaSerasa) >= 0) And
		(@pConsultaSerasaFinal is null Or DateDiff(day,S.DataConsultaSerasa,@pConsultaSerasaFinal) >= 0) And
		(@pPropostaRenovacaoInicial is null Or DateDiff(Day,@pPropostaRenovacaoInicial,PRV.DataCriacao) >= 0) And
		(@pPropostaRenovacaoFinal is null Or DateDiff(Day,PRV.DataCriacao,@pPropostaRenovacaoFinal) >= 0) And
		((@pLCAndamentoSim is null And @pLCAndamentoNao is null) Or (P.ContaClienteID is not null And @pLCAndamentoSim = 1) Or (P.ContaClienteID is null And @pLCAndamentoNao = 1)) And
		((@pAlcadaAndamentoSim is null And @pAlcadaAndamentoNao is null) Or (PA.ContaClienteID is not null And @pAlcadaAndamentoSim = 1) Or (PA.ContaClienteID is null And @pAlcadaAndamentoNao = 1))
	Order By F.NomeCliente

	-- Limpeza
	Drop Table #PropostaRV
	Drop Table #ContaCliente
	Drop Table #GrupoEconomico
	Drop Table #ContaCredito
	Drop Table #Financeiro
	Drop Table #EstruturaComercial
	Drop Table #Representante
	Drop Table #Compras
	Drop Table #Garantias
	Drop Table #Serasa
	Drop Table #PropostaLC
	Drop Table #PropostaAC

END