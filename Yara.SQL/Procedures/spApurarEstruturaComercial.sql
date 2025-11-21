ALTER Procedure [dbo].[spApurarEstruturaComercial]
As
Begin

	-- Tabelas Temporárias
	Create Table #CTC (
		ContaClienteID uniqueidentifier not null,
		EmpresasId char(1) Collate SQL_Latin1_General_CP1_CI_AS,
		CodigoCTC char(3) Collate SQL_Latin1_General_CP1_CI_AS not null,
		DataCriacao datetime not null,
		Primary Key (ContaClienteID, EmpresasId, CodigoCTC))

	Create Table #Representantes (
		ContaClienteID uniqueidentifier not null,
		EmpresasId char(1) Collate SQL_Latin1_General_CP1_CI_AS,
		RepresentanteID uniqueidentifier not null,
		CodigoCTC char(3) Collate SQL_Latin1_General_CP1_CI_AS not null,
		DataCriacao datetime not null,
		Primary Key (ContaClienteID, EmpresasId, RepresentanteID, CodigoCTC))

	-- Variaveis
	Declare @ValidadeEmDiasY int, @ValidadeEmDiasG int

	-- Parâmetros de validade
	Select Top 1 @ValidadeEmDiasY=IsNull(Try_Parse(Valor As int),10)
	From ParametroSistema
	Where Tipo='estcomercial'
	And Chave='validadeEmDias'
	And EmpresasID = 'Y'

	Select Top 1 @ValidadeEmDiasG=IsNull(Try_Parse(Valor As int),10)
	From ParametroSistema
	Where Tipo='estcomercial'
	And Chave='validadeEmDias'
	And EmpresasID = 'G'

	-- Detectar estrutura comercial dos últimos X dias das OVs
	Insert Into #CTC (ContaClienteID,EmpresasId,CodigoCTC,DataCriacao)
	Select Distinct CCC.ContaClienteID, Case When OV.OrgVendas in ('YGA1','YGA2','YGA3') Then 'G' Else 'Y' End, OV.CodigoCtc, Max(OV.DataEmissao)
	From OrdemVenda OV
	Inner Join ContaClienteCodigo CCC On CCC.Codigo = OV.Pagador
	Where OV.DataEmissao > DateAdd(Day, Case When OV.OrgVendas in ('YGA1','YGA2','YGA3') Then -@ValidadeEmDiasG Else -@ValidadeEmDiasY End, getdate())
	And IsNull(OV.CodigoCtc,'') <> ''
	Group By CCC.ContaClienteID, Case When OV.OrgVendas in ('YGA1','YGA2','YGA3') Then 'G' Else 'Y' End, OV.CodigoCtc
	
	-- Detectar estrutura comercial dos últimos X dias adicionadas manualmente
	Insert Into #CTC (ContaClienteID,EmpresasId,CodigoCTC,DataCriacao)
	Select Distinct CCE.ContaClienteID, CCE.EmpresasId, CCE.EstruturaComercialId, CCE.DataCriacao
	From ContaCliente_EstruturaComercial CCE
	Where CCE.DataCriacao > DateAdd(Day, Case When CCE.EmpresasId = 'G' Then -@ValidadeEmDiasG Else -@ValidadeEmDiasY End, getdate())
	And Not Exists (Select 1 From #CTC CX Where CX.ContaClienteID = CCE.ContaClienteId And CX.EmpresasId = CCE.EmpresasId And CX.CodigoCTC = CCE.EstruturaComercialId)

	-- Remover CTCs com Mais de X dias
	Delete CCE
	From ContaCliente_EstruturaComercial CCE
	Left Join #CTC CX On CX.ContaClienteID = CCE.ContaClienteID And CCE.EstruturaComercialID = CX.CodigoCTC And CCE.EmpresasId = CX.EmpresasId
	Where CX.ContaClienteID is null
	And ((CCE.EmpresasId='Y' And CCE.ContaClienteID In (Select Distinct ContaClienteID From #CTC Where EmpresasId='Y'))
	Or (CCE.EmpresasId='G' And CCE.ContaClienteID In (Select Distinct ContaClienteID From #CTC Where EmpresasId='G')))

	-- Inserir novos CTCs (Que não existem)
	Insert Into ContaCliente_EstruturaComercial (ContaClienteID, EmpresasId, EstruturaComercialID, DataCriacao, Ativo)
	Select CX.ContaClienteID, CX.EmpresasId, CX.CodigoCTC, CX.DataCriacao, 1
	From #CTC CX
	Inner Join EstruturaComercial EC On EC.CodigoSap=CX.CodigoCTC And EC.EstruturaComercialPapelID='C'
	Left Join ContaCliente_EstruturaComercial CCE On CCE.ContaClienteID=CX.ContaClienteID And CCE.EstruturaComercialID=CX.CodigoCTC And CCE.EmpresasId = CX.EmpresasId
	Where CCE.ContaClienteID is null

	--- Representantes
	Insert Into #Representantes (ContaClienteID,EmpresasId,RepresentanteID,CodigoCTC,DataCriacao)
	Select CCC.ContaClienteID, Case When OV.OrgVendas in ('YGA1','YGA2','YGA3') Then 'G' Else 'Y' End, R.ID, Min(OV.CodigoCTC), Max(OV.DataEmissao)
	From OrdemVenda OV
	Inner Join ContaClienteCodigo CCC On CCC.Codigo = OV.Pagador
	Inner Join Representante R On (R.CodigoSap = OV.Representante)
	Where OV.DataEmissao > DateAdd(Day, Case When OV.OrgVendas in ('YGA1','YGA2','YGA3') Then -@ValidadeEmDiasG Else -@ValidadeEmDiasY End, getdate())
	And IsNull(OV.Representante,'') <> ''
	Group By CCC.ContaClienteID, Case When OV.OrgVendas in ('YGA1','YGA2','YGA3') Then 'G' Else 'Y' End, R.ID

	-- Detectar representantes dos últimos X dias adicionadas manualmente
	Insert Into #Representantes (ContaClienteID,EmpresasId,RepresentanteID,CodigoCTC,DataCriacao)
	Select CCR.ContaClienteID, CCR.EmpresasId, CCR.RepresentanteID, CCR.CodigoSapCTC, CCR.DataCriacao
	From ContaCliente_Representante CCR
	Where CCR.DataCriacao > DateAdd(Day, Case When CCR.EmpresasId = 'G' Then -@ValidadeEmDiasG Else -@ValidadeEmDiasY End, getdate())
	And Not Exists (Select 1 From #Representantes RX Where RX.ContaClienteID = CCR.ContaClienteId And RX.EmpresasId = CCR.EmpresasId And RX.RepresentanteID = CCR.RepresentanteID And RX.CodigoCTC = CCR.CodigoSapCTC)

	-- Remover Representantes com Mais de X dias
	Delete CR
	From ContaCliente_Representante CR
	Left Join #Representantes RX On RX.ContaClienteID = CR.ContaClienteID And RX.RepresentanteID = CR.RepresentanteID And RX.EmpresasId = CR.EmpresasID And RX.CodigoCTC = CR.CodigoSapCTC
	Where RX.ContaClienteID is null
	And ((CR.EmpresasId='Y' And CR.ContaClienteID In (Select Distinct ContaClienteID From #Representantes Where EmpresasId='Y'))
	Or (CR.EmpresasId='G' And CR.ContaClienteID In (Select Distinct ContaClienteID From #Representantes Where EmpresasId='G')))

	-- Inserir novos Representantes (Que não existem)
	Insert Into ContaCliente_Representante (ContaClienteID,EmpresasId,RepresentanteID,CodigoSapCTC,DataCriacao, Ativo)
	Select RX.ContaClienteID, RX.EmpresasId ,RX.RepresentanteID, RX.CodigoCTC, RX.DataCriacao, 1
	From #Representantes RX
	Inner Join Representante R On R.ID = RX.RepresentanteID
	Left Join ContaCliente_Representante CR On CR.ContaClienteID = RX.ContaClienteID And CR.RepresentanteID = RX.RepresentanteID And RX.EmpresasId = CR.EmpresasID
	Where CR.ContaClienteID is null

	-- Limpeza
	Drop Table #CTC
	Drop Table #Representantes

	-- Criar Conta Cliente Financeiros
	Insert Into ContaClienteFinanceiro(ContaClienteID,EmpresasID,UsuarioIDCriacao,DataCriacao,LC,Exposicao,Conceito,Sinistro,DividaAtiva,ConceitoAnterior)
	Select ID,'Y','00000000-0000-0000-0000-000000000000',getdate(),0,0,0,0,0,0
	From ContaCliente
	Where ID Not In
	(
	Select ContaClienteID From ContaClienteFinanceiro Where EmpresasID='Y'
	)

	Insert Into ContaClienteFinanceiro(ContaClienteID,EmpresasID,UsuarioIDCriacao,DataCriacao,LC,Exposicao,Conceito,Sinistro,DividaAtiva,ConceitoAnterior)
	Select ID,'G','00000000-0000-0000-0000-000000000000',getdate(),0,0,0,0,0,0
	From ContaCliente
	Where ID Not In
	(
	Select ContaClienteID From ContaClienteFinanceiro Where EmpresasID='G'
	)

	-- Criar Perfil x Usuario
	Insert Into EstruturaPerfilUsuario (ID,CodigoSap,PerfilId,UsuarioIDCriacao,DataCriacao)
	Select newid(),EC.CodigoSap, P.ID,'00000000-0000-0000-0000-000000000000',getdate()
	From EstruturaComercial EC
	Inner JOin Perfil P ON (1=1)
	Where EC.EstruturaComercialPapelID='C'
	And EC.Ativo=1
	And EC.CodigoSap not In (Select Distinct EPU.CodigoSap From EstruturaPerfilUsuario EPU)

End
