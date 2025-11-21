ALTER Procedure [dbo].[spCobranca_EfetivaPorDiretoria]
	@EmpresaId char(1),
	@DiretoriaId varchar(10) = null
AS
BEGIN
	Set NOCOUNT On

	-- Variaveis
	Declare @DiasVencidos int
	Declare @EmpresaTitulo varchar(4)
	Declare @TiposTituloCobrancaVal varchar(max)
	Declare @ValorTotal decimal(18,9)
	Declare @StatusCobrancaIDPadrao uniqueidentifier
	DECLARE @CondicaoAVista varchar(max)

	-- Buscar parâmetro de condições a vista
	SELECT TOP 1 @CondicaoAVista = Valor FROM ParametroSistema WHERE Tipo = 'ordem' AND Chave = 'condicaovista' AND EmpresasID = @EmpresaID

	-- Tabelas Temporárias
	CREATE TABLE #TiposTitulosCobranca (TipoDocumento varchar(2) COLLATE DATABASE_DEFAULT)
	CREATE TABLE #Titulos (ContaClienteId uniqueidentifier, DataVencimento datetime, Valor decimal(18,9), DiretoriaId varchar(10) COLLATE DATABASE_DEFAULT, Diretoria varchar(120) COLLATE DATABASE_DEFAULT, CTC varchar(10) COLLATE DATABASE_DEFAULT, StatusCobrancaID uniqueidentifier)
	CREATE TABLE #Diretorias (ContaClienteId uniqueidentifier, CTC varchar(10) COLLATE DATABASE_DEFAULT, DiretoriaId varchar(10) COLLATE DATABASE_DEFAULT, Diretoria varchar(120) COLLATE DATABASE_DEFAULT)
	CREATE TABLE #Resultado (Tipo char(2) COLLATE DATABASE_DEFAULT, Chave varchar(120) COLLATE DATABASE_DEFAULT, Nome varchar(120) COLLATE DATABASE_DEFAULT, Dias30 decimal(18,9), Dias60 decimal(18,9), Dias90 decimal(18,9), Dias180 decimal(18,9), DiasMais decimal(18,9), Total decimal(18,9), Percentual decimal(13,2))

	-- Buscar Parametros de Cobrança
	Select Top 1 @DiasVencidos=IsNull(Try_Parse(Valor As int),10)
	From ParametroSistema
	Where Tipo='cobranca'
	And Chave='cdias'
	And EmpresasID = @EmpresaID

	Select Top 1 @TiposTituloCobrancaVal=IsNull(Valor,'')
	From ParametroSistema
	Where Tipo='cobranca'
	And Chave='ctipos'
	And EmpresasID = @EmpresaID

	Select Top 1 @StatusCobrancaIDPadrao=ID
	From StatusCobranca
	Where Padrao=1

	Insert Into #TiposTitulosCobranca
	Select Left(splitdata,2) as TipoDocumento
	From dbo.fnSplitString(@TiposTituloCobrancaVal, ',')

	-- Determinar Parametros de Empresas
	If @EmpresaID = 'G'
	Begin
		Set @EmpresaTitulo = 'GICS'
	End
	Else
	Begin
		Set @EmpresaTitulo = 'TRV'
	End

	Print 'Dias Vencimento Cobranca: ' + Convert(varchar(max), @DiasVencidos)
	Print 'Tipos Titulo Cobranca: ' + @TiposTituloCobrancaVal
	Print 'Empresa Cobranca: ' + @EmpresaTitulo

	-- Buscar Titulos Vencidos (Em Aberto)
	Insert Into #Titulos (ContaClienteId, DataVencimento, Valor,CTC,StatusCobrancaID)
	Select CC.ID, T.DataVencimento, T.ValorInterno/1000000, 
	CASE T.TipoDocumento 
		WHEN 'ZC' 
		THEN (SELECT TOP 1 EstruturaComercialId FROM ContaCliente_EstruturaComercial CCE WHERE CCE.ContaClienteId = CC.ID AND CCE.EmpresasId = @EmpresaId AND CCE.Ativo = 1)
		ELSE OV.CodigoCtc
		END,	
	IsNull(T.StatusCobrancaID,@StatusCobrancaIDPadrao)
	From Titulo T
	Inner Join ContaClienteCodigo CCC On (CCC.Codigo = T.CodigoCliente)
	Inner Join ContaCliente CC On (CC.ID = CCC.ContaClienteID)
	Inner Join #TiposTitulosCobranca TTC On (TTC.TipoDocumento = T.TipoDocumento)
	Inner Join StatusCobranca SC On (SC.ID = IsNull(T.StatusCobrancaID,@StatusCobrancaIDPadrao))
	Left Join OrdemVenda OV On (OV.Numero=T.OrdemVendaNumero)
	Where T.Empresa = @EmpresaTitulo
	And T.Aberto = 1
	And T.CreditoDebito='S'
	And (T.PropostaStatus <> 'J' OR T.PropostaStatus IS NULL)
	And DateDiff(Day,T.DataVencimento,getdate()) > @DiasVencidos
	And SC.CobrancaEfetiva = 1
	AND T.CondPagto NOT IN (SELECT splitdata FROM dbo.fnSplitString(@CondicaoAVista, ','))

	-- Buscar Diretorias (Com CTC na OV)
	Insert Into #Diretorias (ContaClienteId, CTC, DiretoriaId)
	Select ContaClienteId, T.CTC, ED.CodigoSap
	From #Titulos T
	Inner Join EstruturaComercial EC On (EC.CodigoSap = T.CTC And EC.EstruturaComercialPapelID='C')
	Inner Join EstruturaComercial EG On (EG.CodigoSap = EC.Superior_CodigoSap And EG.EstruturaComercialPapelID='G')
	Inner Join EstruturaComercial ED On (ED.CodigoSap = EG.Superior_CodigoSap And ED.EstruturaComercialPapelID='D')

	-- Buscar Nomes das Diretorias
	Update D Set D.Diretoria = ED.Nome
	From #Diretorias D
	Inner Join EstruturaComercial ED On (ED.CodigoSap = D.DiretoriaId And ED.EstruturaComercialPapelID='D')

	-- Cruzar Titulos com Diretorias
	Update T Set T.DiretoriaId = IsNull(D.DiretoriaId,''), T.Diretoria = IsNull(D.Diretoria,'')
	From #Titulos T
	Left Join #Diretorias D On  (D.ContaClienteId = T.ContaClienteId And D.CTC = T.CTC)

	If @DiretoriaId Is Not Null
	Begin

		Delete From #Diretorias Where DiretoriaId <> @DiretoriaId
		Delete From #Titulos Where DiretoriaId <> @DiretoriaId

	End

	Insert Into #Resultado (Chave,Nome)
	Select Distinct T.DiretoriaId, T.Diretoria
	From #Titulos T

	Select @ValorTotal = Sum(Valor)
	From #Titulos

	Update R Set R.Dias30 = TX.Valor
	From #Resultado R
	Inner Join (Select DiretoriaId, IsNull(Sum(Valor),0) as Valor From #Titulos Where DateDiff(Day,DataVencimento,getdate()) <= 30 Group By DiretoriaId) TX On (TX.DiretoriaId = R.Chave)

	Update R Set R.Dias60 = TX.Valor
	From #Resultado R
	Inner Join (Select DiretoriaId, IsNull(Sum(Valor),0) as Valor From #Titulos Where DateDiff(Day,DataVencimento,getdate()) > 30 And DateDiff(Day,DataVencimento,getdate()) <= 60 Group By DiretoriaId) TX On (TX.DiretoriaId = R.Chave)

	Update R Set R.Dias90 = TX.Valor
	From #Resultado R
	Inner Join (Select DiretoriaId, IsNull(Sum(Valor),0) as Valor From #Titulos Where DateDiff(Day,DataVencimento,getdate()) > 60 And DateDiff(Day,DataVencimento,getdate()) <= 90 Group By DiretoriaId) TX On (TX.DiretoriaId = R.Chave)

	Update R Set R.Dias180 = TX.Valor
	From #Resultado R
	Inner Join (Select DiretoriaId, IsNull(Sum(Valor),0) as Valor From #Titulos Where DateDiff(Day,DataVencimento,getdate()) > 90 And DateDiff(Day,DataVencimento,getdate()) <= 180 Group By DiretoriaId) TX On (TX.DiretoriaId = R.Chave)

	Update R Set R.DiasMais = TX.Valor
	From #Resultado R
	Inner Join (Select DiretoriaId, IsNull(Sum(Valor),0) as Valor From #Titulos Where DateDiff(Day,DataVencimento,getdate()) > 180 Group By DiretoriaId) TX On (TX.DiretoriaId = R.Chave)

	Update #Resultado Set Dias30=IsNull(Dias30,0), Dias60=IsNull(Dias60,0), Dias90=IsNull(Dias90,0), Dias180=IsNull(Dias180,0), DiasMais=IsNull(DiasMais,0)

	Update #Resultado Set Total=Dias30+Dias60+Dias90+Dias180+DiasMais, Percentual=Iif(@ValorTotal>0,Round((((Dias30+Dias60+Dias90+Dias180+DiasMais)*100)/@ValorTotal),2,0),0)

	Update #Resultado Set Tipo='ED'

	Select *
	From #Resultado
	Order By Percentual Desc

	-- Limpeza
	Drop Table #TiposTitulosCobranca
	Drop Table #Titulos
	Drop Table #Diretorias
	Drop Table #Resultado
END