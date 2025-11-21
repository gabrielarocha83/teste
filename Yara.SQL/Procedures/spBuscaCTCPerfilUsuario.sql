ALTER PROCEDURE [dbo].[spBuscaCTCPerfilUsuario]
	@Usuario VARCHAR(120) = NULL,
	@CTC VARCHAR(120) = NULL,
	@GC VARCHAR(120) = NULL
AS 
BEGIN

	SELECT  
		E.CodigoSap AS CodCTC, 
		C.Nome AS CTC, 
		G.Nome AS GC, 
		D.Nome AS DI, 
		P.ID AS PerfilID, 
		P.Descricao AS Perfil, 
		E.UsuarioId 
	FROM EstruturaPerfilUsuario E 
	INNER JOIN EstruturaComercial C 
		ON C.CodigoSap = E.CodigoSap
	INNER JOIN Perfil P 
		ON P.ID = E.PerfilId
	LEFT JOIN Usuario U 
		ON U.ID = E.UsuarioId
	INNER JOIN EstruturaComercial G 
		ON  C.Superior_CodigoSap = G.CodigoSap  
		AND G.EstruturaComercialPapelID = 'G'
	LEFT JOIN EstruturaComercial D 
		ON  G.Superior_CodigoSap = D.CodigoSap  
		AND D.EstruturaComercialPapelID = 'D'
	WHERE 
		(@CTC IS NULL OR (E.CodigoSAP = @CTC OR C.Nome LIKE '%'+ @CTC +'%'))
		AND (@Usuario IS NULL OR ( U.Nome LIKE '%' + @Usuario + '%' OR U.Login = @Usuario))
		AND (@GC IS NULL OR (G.CodigoSap = @GC OR G.Nome LIKE '%' + @GC + '%'))
	ORDER BY P.Ordem

END