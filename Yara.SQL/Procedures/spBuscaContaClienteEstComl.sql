ALTER PROCEDURE [dbo].[spBuscaContaClienteEstComl]
(
 @pNome				VARCHAR(120) = NULL,
 @pDocumento		VARCHAR(120) = NULL,
 @pCodigo			VARCHAR(10)  = NULL,
 @pApelido			VARCHAR(120) = NULL,
 @pRepresentante	VARCHAR(120) = NULL,
 @pCTC				VARCHAR(120) = NULL,
 @pGC				VARCHAR(120) = NULL,
 @pDC				VARCHAR(120) = NULL,
 @pEmpresaId		VARCHAR(1)   = NULL,
 @pAtivo			BIT			 = NULL
)
AS
BEGIN

	Declare @lNome varchar(120)
	Declare @lDocumento varchar(120)
	Declare @lCodigo varchar(10)
	Declare @lApelido varchar(120)
	Declare @lRepresentante varchar(120)
	Declare @lCTC varchar(120)
	Declare @lGC varchar(120)
	Declare @lDC varchar(120)
	Declare @lEmpresaId varchar(1)
	Declare @lAtivo bit

	Set @lNome=@pNome
	Set @lDocumento=@pDocumento
	Set @lCodigo=@pCodigo
	Set @lApelido=@pApelido
	Set @lRepresentante=@pRepresentante
	Set @lCTC=@pCTC
	Set @lGC=@pGC
	Set @lDC=@pDC
	Set @lEmpresaId=@pEmpresaId
	Set @lAtivo = @pAtivo

	SELECT DISTINCT
		CC.ID, 
		CC.CodigoPrincipal, 
		CC.Nome, 
		CC.Documento, 
		ISNULL(R.Nome,'') as Representante, 
		ISNULL(ECC.Nome,'') as CTC, 
		ISNULL(ECG.Nome,'') as GC, 
		ISNULL(ECD.Nome,'') as DC, 
		ISNULL(CCE.EstruturaComercialId,'') as EstruturaComercialId, 
		CCR.RepresentanteID,
		ISNULL(CCE.Ativo, 1) AS Ativo
	FROM ContaCliente CC WITH (NOLOCK)
	LEFT JOIN ContaCliente_EstruturaComercial CCE WITH (NOLOCK)
		ON CCE.ContaClienteId = CC.ID 
		AND CCE.EmpresasId = @lEmpresaId
		AND CCE.Ativo = @lAtivo
	LEFT JOIN ContaCliente_Representante CCR WITH (NOLOCK)
		ON CCR.ContaClienteID = CC.ID 
		AND CCR.CodigoSapCTC = CCE.EstruturaComercialId
		AND CCR.EmpresasID = @lEmpresaId
		AND CCR.Ativo = @lAtivo
	LEFT JOIN EstruturaComercial ECC WITH (NOLOCK) 
		ON ECC.CodigoSap = CCE.EstruturaComercialId
	LEFT JOIN EstruturaComercial ECG WITH (NOLOCK) 
		ON ECG.CodigoSap = ECC.Superior_CodigoSap
	LEFT JOIN EstruturaComercial ECD WITH (NOLOCK) 
		ON ECD.CodigoSap = ECG.Superior_CodigoSap
	LEFT JOIN Representante R WITH (NOLOCK)
		ON R.ID = CCR.RepresentanteID
	WHERE (@lNome Is Null Or Lower(CC.Nome) like '%'+Lower(@lNome)+'%')
		AND (@lDocumento Is Null Or Exists(Select 1 From ContaClienteCodigo CCC Where CCC.Documento like '%'+@lDocumento+'%' And CCC.ContaClienteID = CC.ID))
		AND (@lCodigo Is Null Or Exists(Select 1 From ContaClienteCodigo CCC Where CCC.Codigo like '%'+@lCodigo+'%' And CCC.ContaClienteID = CC.ID))
		AND (@lApelido Is Null Or Lower(CC.Apelido) like '%'+Lower(@lApelido)+'%')
		AND (@lRepresentante Is Null Or Lower(R.Nome) like '%'+Lower(@lRepresentante)+'%' Or Lower(R.CodigoSap) like '%'+Lower(@lRepresentante)+'%')
		AND (@lCTC Is Null Or Lower(ECC.Nome) like '%'+Lower(@lCTC)+'%' Or Lower(ECC.CodigoSap) like '%'+Lower(@lCTC)+'%')
		AND (@lGC Is Null Or Lower(ECG.Nome) like '%'+Lower(@lGC)+'%' Or Lower(ECG.CodigoSap) like '%'+Lower(@lGC)+'%')
		AND (@lDC Is Null Or Lower(ECD.Nome) like '%'+Lower(@lDC)+'%' Or Lower(ECD.CodigoSap) like '%'+Lower(@lDC)+'%')

END