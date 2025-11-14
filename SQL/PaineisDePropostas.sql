--YMPCC-192
IF NOT EXISTS (SELECT * FROM [dbo].[Permissao] WHERE [Nome] = 'ResumoAnalises_Visualizar')
BEGIN
	INSERT INTO [dbo].[Permissao]([Nome],[Descricao],[Ativo],[Processo])
	VALUES ('ResumoAnalises_Visualizar','Visualizar Aba Resumo de Análises',1,'Resumo de Análises')
END
IF NOT EXISTS (SELECT * FROM [dbo].[Permissao] WHERE [Nome] = 'ResumoAprovacoes_Visualizar')
BEGIN
	INSERT INTO [dbo].[Permissao]([Nome],[Descricao],[Ativo],[Processo])
	VALUES ('ResumoAprovacoes_Visualizar','Visualizar Aba Resumo de Aprovações',1,'Resumo de Aprovações')
END

--YMPCC-193
IF NOT EXISTS (SELECT * FROM [dbo].[Permissao] WHERE [Nome] = 'ResumoAnalises_LeadtimePreAnalise')
BEGIN
	INSERT INTO [dbo].[Permissao]([Nome],[Descricao],[Ativo],[Processo])
	VALUES ('ResumoAnalises_LeadtimePreAnalise','Visualizar Leadtime Médio Geral de Pré-Análise',1,'Resumo de Análises')
END
IF NOT EXISTS (SELECT * FROM [dbo].[Permissao] WHERE [Nome] = 'ResumoAnalises_LeadtimeAnalise')
BEGIN
	INSERT INTO [dbo].[Permissao]([Nome],[Descricao],[Ativo],[Processo])
	VALUES ('ResumoAnalises_LeadtimeAnalise','Visualizar Leadtime Médio Geral de Análise',1,'Resumo de Análises')
END
IF NOT EXISTS (SELECT * FROM [dbo].[Permissao] WHERE [Nome] = 'ResumoAnalises_PropostasEmPreAnalise')
BEGIN
	INSERT INTO [dbo].[Permissao]([Nome],[Descricao],[Ativo],[Processo])
	VALUES ('ResumoAnalises_PropostasEmPreAnalise','Visualizar Quantidade de Propostas em Pré-Análise',1,'Resumo de Análises')
END
IF NOT EXISTS (SELECT * FROM [dbo].[Permissao] WHERE [Nome] = 'ResumoAnalises_PropostasEnviadasPreAnalise')
BEGIN
	INSERT INTO [dbo].[Permissao]([Nome],[Descricao],[Ativo],[Processo])
	VALUES ('ResumoAnalises_PropostasEnviadasPreAnalise','Visualizar Quantidade de Propostas Enviadas para Pré-Análise',1,'Resumo de Análises')
END
IF NOT EXISTS (SELECT * FROM [dbo].[Permissao] WHERE [Nome] = 'ResumoAnalises_PropostasEmAnalise')
BEGIN
	INSERT INTO [dbo].[Permissao]([Nome],[Descricao],[Ativo],[Processo])
	VALUES ('ResumoAnalises_PropostasEmAnalise','Visualizar Quantidade de Propostas em Análise',1,'Resumo de Análises')
END
IF NOT EXISTS (SELECT * FROM [dbo].[Permissao] WHERE [Nome] = 'ResumoAnalises_PropostasEnviadasAnalise')
BEGIN
	INSERT INTO [dbo].[Permissao]([Nome],[Descricao],[Ativo],[Processo])
	VALUES ('ResumoAnalises_PropostasEnviadasAnalise','Visualizar Quantidade de Propostas Enviadas para Análise',1,'Resumo de Análises')
END
IF NOT EXISTS (SELECT * FROM [dbo].[Permissao] WHERE [Nome] = 'ResumoAnalises_PropostasAlcadaAnalise')
BEGIN
	INSERT INTO [dbo].[Permissao]([Nome],[Descricao],[Ativo],[Processo])
	VALUES ('ResumoAnalises_PropostasAlcadaAnalise','Visualizar Quantidade de Propostas por Alçada de Análise',1,'Resumo de Análises')
END

--YMPCC-204
IF NOT EXISTS (SELECT * FROM [dbo].[Permissao] WHERE [Nome] = 'ResumoAprovacoes_LeadtimeAprovacao')
BEGIN
	INSERT INTO [dbo].[Permissao]([Nome],[Descricao],[Ativo],[Processo])
	VALUES ('ResumoAprovacoes_LeadtimeAprovacao','Visualizar Leadtime Médio Geral de Aprovação',1,'Resumo de Aprovações')
END
IF NOT EXISTS (SELECT * FROM [dbo].[Permissao] WHERE [Nome] = 'ResumoAprovacoes_PropostasAguardandoAcaoAnalista')
BEGIN
	INSERT INTO [dbo].[Permissao]([Nome],[Descricao],[Ativo],[Processo])
	VALUES ('ResumoAprovacoes_PropostasAguardandoAcaoAnalista','Visualizar Quantidade de Propostas Aguardando Ação do Analista',1,'Resumo de Aprovações')
END
IF NOT EXISTS (SELECT * FROM [dbo].[Permissao] WHERE [Nome] = 'ResumoAprovacoes_PropostasAprovadas')
BEGIN
	INSERT INTO [dbo].[Permissao]([Nome],[Descricao],[Ativo],[Processo])
	VALUES ('ResumoAprovacoes_PropostasAprovadas','Visualizar Quantidade de Propostas Aprovadas',1,'Resumo de Aprovações')
END
IF NOT EXISTS (SELECT * FROM [dbo].[Permissao] WHERE [Nome] = 'ResumoAprovacoes_PropostasAprovadasPendencia')
BEGIN
	INSERT INTO [dbo].[Permissao]([Nome],[Descricao],[Ativo],[Processo])
	VALUES ('ResumoAprovacoes_PropostasAprovadasPendencia','Visualizar Quantidade de Propostas Aprovadas Com Pendência',1,'Resumo de Aprovações')
END
IF NOT EXISTS (SELECT * FROM [dbo].[Permissao] WHERE [Nome] = 'ResumoAprovacoes_PropostasAprovadasPendenciaGarant')
BEGIN
	INSERT INTO [dbo].[Permissao]([Nome],[Descricao],[Ativo],[Processo])
	VALUES ('ResumoAprovacoes_PropostasAprovadasPendenciaGarant','Visualizar Quantidade de Propostas Aprovadas Com Pendência de Garantia',1,'Resumo de Aprovações')
END
IF NOT EXISTS (SELECT * FROM [dbo].[Permissao] WHERE [Nome] = 'ResumoAprovacoes_PropostasEmAprovacao')
BEGIN
	INSERT INTO [dbo].[Permissao]([Nome],[Descricao],[Ativo],[Processo])
	VALUES ('ResumoAprovacoes_PropostasEmAprovacao','Visualizar Quantidade de Propostas em Aprovação',1,'Resumo de Aprovações')
END
IF NOT EXISTS (SELECT * FROM [dbo].[Permissao] WHERE [Nome] = 'ResumoAprovacoes_PropostasAlcadaAprovacao')
BEGIN
	INSERT INTO [dbo].[Permissao]([Nome],[Descricao],[Ativo],[Processo])
	VALUES ('ResumoAprovacoes_PropostasAlcadaAprovacao','Visualizar Quantidade de Propostas por Alçada de Aprovação',1,'Resumo de Aprovações')
END

--YMPCC-6
CREATE FUNCTION [dbo].[CalcularDiasUteis] 
(
	@DataInicial datetime,
	@DataFinal datetime
)
RETURNS int
AS
BEGIN
  DECLARE @DiasUteis int = 0
  DECLARE @Data datetime = @DataInicial

  WHILE @Data <= @DataFinal
  BEGIN
	IF (DATEPART(WEEKDAY, @Data) NOT IN (1,7))
	BEGIN
	  SET @DiasUteis = @DiasUteis + 1
	END
	SET @Data = DATEADD(day, 1, @Data)
  END

  DECLARE @FeriadoDias int = ISNULL((SELECT COUNT(DISTINCT(DataFeriado)) 
	   								   FROM Feriado 
									  WHERE Ativo = 1 
								        and DataFeriado BETWEEN @DataInicial and @DataFinal
									    and DATEPART(WEEKDAY, Dataferiado) NOT IN (1,7)), 0)
  RETURN @DiasUteis - @FeriadoDias
END

--YMPCC-6
CREATE FUNCTION [dbo].[LeadtimePropostaLCPorStatus] 
(
	@PropostaLCID uniqueidentifier,
	@PropostaLCStatusID varchar(2)
)
RETURNS int
AS
BEGIN
    DECLARE @LeadtimeDias int = 0
	
	IF (ISNULL(@PropostaLCStatusID, '') = '')
	BEGIN
	  SET @PropostaLCStatusID = (SELECT PropostaLCStatusID 
								   FROM PropostaLC 
								  WHERE ID = @PropostaLCID)
	END

    DECLARE @DataStatus datetime = CONVERT(date, (SELECT TOP 1 DataCriacao
								      FROM PropostaLCHistorico
							         WHERE PropostaLCID = @PropostaLCID
								       and PropostaLCStatusID = @PropostaLCStatusID
							      ORDER BY DataCriacao DESC))
    IF (@DataStatus IS NOT NULL)
	BEGIN
	  DECLARE @DataAtual datetime = CONVERT(date, GETDATE())

	  SET @LeadtimeDias = dbo.CalcularDiasUteis(@DataStatus, @DataAtual)
	END
    
	RETURN @LeadtimeDias
END

--YMPCC-6
CREATE PROCEDURE [dbo].[spBuscaPropostaLCPorStatus]
	@EmpresaID CHAR(1),
	@PropostaLCStatusIDs VARCHAR(100)
AS
BEGIN
	   SELECT P.Descricao,
	          F.ValorAte,
		      F.Nivel,
		      F.SegmentoID
         INTO #FAAN
	     FROM FluxoAlcadaAnalise F
   INNER JOIN Perfil P ON P.ID = F.PerfilID
	    WHERE F.EmpresaID = @EmpresaID
	      AND F.Ativo = 1
	      AND P.Descricao in ('Analista de Crédito Jr', 'Analista de Crédito Pl', 'Analista de Crédito Sr')
     ORDER BY Nivel ASC

	   SELECT P.Descricao,
	          F.ValorAte,
		      F.Nivel,
		      F.SegmentoID
         INTO #FAAP
	     FROM FluxoAlcadaAprovacao F
   INNER JOIN Perfil P ON P.ID = F.PerfilID
	    WHERE F.EmpresaID = @EmpresaID
	      AND F.Ativo = 1
	      AND P.Descricao in ('Analista de Crédito Jr', 'Analista de Crédito Pl', 'Analista de Crédito Sr')
     ORDER BY Nivel ASC

	 SELECT DISTINCT
			P.ID,
			CC.ID AS ContaClienteID, 
			'LC' + RIGHT(REPLICATE('0',5) + CONVERT(VARCHAR(MAX),P.NumeroInternoProposta),5) +'/' + CONVERT(VARCHAR,YEAR(P.DataCriacao)) AS Codigo, 
			CC.Nome AS Nome, 
			C.Nome AS CTC, 
			G.Nome AS GC, 
			R.Nome AS Representante,
			S.Nome AS [Status],
			P.PropostaLCStatusID,
			ISNULL(P.LCProposto, 0) AS Valor, 
 		    dbo.LeadtimePropostaLCPorStatus(P.ID, P.PropostaLCStatusID) AS LeadTime, 
			U.Nome AS Responsavel,
	        ISNULL((SELECT TOP 1 FA.Descricao
			   			  FROM #FAAN FA
						 WHERE FA.SegmentoID = CC.SegmentoID  
						   AND FA.ValorAte >= ISNULL(P.LCProposto, 0)
					  ORDER BY Nivel ASC),'') AS AlcadaAnalise,
			ISNULL((SELECT TOP 1 FA.Descricao
					  FROM #FAAP FA
			   	     WHERE FA.SegmentoID = CC.SegmentoID  
					   AND FA.ValorAte >= ISNULL(P.LCProposto, 0)
			   	  ORDER BY Nivel ASC),'') AS AlcadaAprovacao
	   FROM PropostaLC P 
 INNER JOIN PropostaLCStatus S ON S.ID = P.PropostaLCStatusID 
 INNER JOIN ContaCliente CC ON CC.ID = P.ContaClienteID 
  LEFT JOIN Representante R ON R.ID = P.RepresentanteID
  LEFT JOIN EstruturaComercial C ON C.CodigoSap = P.CodigoSap 
  LEFT JOIN EstruturaComercial G ON G.CodigoSap = C.Superior_CodigoSap 
  LEFT JOIN Usuario U ON U.ID = P.ResponsavelID 
   	  WHERE P.EmpresaID = @EmpresaID 
		AND P.PropostaLCStatusID IN (SELECT splitdata FROM fnSplitString(@PropostaLCStatusIDs, ','))

	   DROP TABLE #FAAN
	   DROP TABLE #FAAP
END

--YMPCC-6
-- =============================================
-- Author:		Andre Paganuchi (PerformaIT)
-- Create date: 2017-12-26
-- Update date: 2022-07-23 (Fabio)
-- Description:	Calcular Leadtime Proposta LC
-- =============================================
ALTER FUNCTION [dbo].[LeadtimePropostaLC] 
(
	@PropostaLCID uniqueidentifier
)
RETURNS int
AS
BEGIN
       Declare  @dataInicio datetime, @dataTermino datetime
       --Declare @diasFimDeSemana int, @diasFeriado int, @diasLeadtime int

       Set @dataInicio = [dbo].[LeadtimeDatePropostaLC](@PropostaLCID, 0)
       Set @dataTermino = [dbo].[LeadtimeDatePropostaLC](@PropostaLCID ,1)

	   RETURN dbo.CalcularDiasUteis(CONVERT(date, @dataInicio), CONVERT(date, @dataTermino))

	   /*
       -- Calcular Lead Time
       Set @diasLeadtime = convert(int,convert(decimal(20,10), @dataTermino-@dataInicio))

       -- Fins de Semana
       Set @diasFimDeSemana = DATEDIFF(WK, @dataInicio, @dataTermino) * 2 - Case When DATEPART(DW, @dataInicio) = 1 Then 1 Else 0 End + Case When DATEPART(DW, @dataTermino) = 1 Then 1 Else 0 End

       -- Feriados
       Select @diasFeriado = Count(1) From Feriado Where Ativo = 1 And DataFeriado Between @dataInicio And @dataTermino

       Return IsNull(@diasLeadtime, 0) - IsNull(@diasFimDeSemana, 0) - IsNull(@diasFeriado, 0)
	   */
END

--YMPCC-6
-- =============================================
-- Author:		Andre Paganuchi (PerformaIT)
-- Create date: 2017-12-26
-- Update date: 2022-07-23 (Fabio)
-- Description:	Calcular Leadtime Proposta LC
-- =============================================
ALTER FUNCTION [dbo].[LeadtimePropostaLCAdicional] 
(
	@PropostaLCAdicionalID uniqueidentifier
)
RETURNS int
AS
BEGIN

	Declare  @dataInicio datetime, @dataTermino datetime
	--Declare @diasFimDeSemana int, @diasFeriado int, @diasLeadtime int

	Set @dataInicio = [dbo].[LeadtimeDatePropostaLCAdicional](@PropostaLCAdicionalID, 0)
	Set @dataTermino = [dbo].[LeadtimeDatePropostaLCAdicional](@PropostaLCAdicionalID ,1)

	RETURN dbo.CalcularDiasUteis(CONVERT(date, @dataInicio), CONVERT(date, @dataTermino))

	/*
	-- Calcular Lead Time
	Set @diasLeadtime = DATEDIFF(DAY, @dataInicio, @dataTermino)

	-- Fins de Semana
	Set @diasFimDeSemana = DATEDIFF(WK, @dataInicio, @dataTermino) * 2 - Case When DATEPART(DW, @dataInicio) = 1 Then 1 Else 0 End + Case When DATEPART(DW, @dataTermino) = 1 Then 1 Else 0 End

	-- Feriados
	Select @diasFeriado = Count(1) From Feriado Where Ativo = 1 And DataFeriado Between @dataInicio And @dataTermino

	Return IsNull(@diasLeadtime, 0) - IsNull(@diasFimDeSemana, 0) - IsNull(@diasFeriado, 0)
	*/
END