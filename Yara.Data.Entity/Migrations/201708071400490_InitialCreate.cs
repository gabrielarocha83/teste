namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AreaIrrigada",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Nome = c.String(nullable: false, maxLength: 50, unicode: false),
                        Ativo = c.Boolean(nullable: false),
                        UsuarioIDCriacao = c.Guid(nullable: false),
                        UsuarioIDAlteracao = c.Guid(),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ConceitoCobranca",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Nome = c.String(nullable: false, maxLength: 2, unicode: false),
                        Descricao = c.String(maxLength: 120, unicode: false),
                        Ativo = c.Boolean(nullable: false),
                        UsuarioIDCriacao = c.Guid(nullable: false),
                        UsuarioIDAlteracao = c.Guid(),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.Nome, unique: true, name: "Index");
            
            CreateTable(
                "dbo.ContaCliente",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Documento = c.String(nullable: false, maxLength: 24, unicode: false),
                        CodigoPrincipal = c.String(nullable: false, maxLength: 10, unicode: false),
                        Nome = c.String(nullable: false, maxLength: 120, unicode: false),
                        Apelido = c.String(maxLength: 120, unicode: false),
                        TipoClienteID = c.Guid(nullable: false),
                        DataNascimentoFundacao = c.DateTime(),
                        Contato = c.String(maxLength: 120, unicode: false),
                        CEP = c.String(maxLength: 120, unicode: false),
                        Endereco = c.String(maxLength: 120, unicode: false),
                        Numero = c.String(maxLength: 64, unicode: false),
                        Complemento = c.String(maxLength: 64, unicode: false),
                        Bairro = c.String(maxLength: 120, unicode: false),
                        Cidade = c.String(maxLength: 120, unicode: false),
                        UF = c.String(maxLength: 120, unicode: false),
                        Pais = c.String(maxLength: 120, unicode: false),
                        Telefone = c.String(maxLength: 120, unicode: false),
                        Email = c.String(maxLength: 120, unicode: false),
                        UsuarioIDCriacao = c.Guid(nullable: false),
                        UsuarioIDAlteracao = c.Guid(),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.TipoCliente", t => t.TipoClienteID)
                .Index(t => t.TipoClienteID);
            
            CreateTable(
                "dbo.EstruturaComercial",
                c => new
                    {
                        CodigoSap = c.String(nullable: false, maxLength: 10, unicode: false),
                        UsuarioIDCriacao = c.Guid(nullable: false),
                        UsuarioIDAlteracao = c.Guid(),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(),
                        Nome = c.String(nullable: false, maxLength: 120, unicode: false),
                        Ativo = c.Boolean(nullable: false),
                        EstruturaComercialPapelID = c.String(nullable: false, maxLength: 1, fixedLength: true, unicode: false),
                        Superior_CodigoSap = c.String(maxLength: 10, unicode: false),
                    })
                .PrimaryKey(t => t.CodigoSap)
                .ForeignKey("dbo.EstruturaComercialPapel", t => t.EstruturaComercialPapelID)
                .ForeignKey("dbo.EstruturaComercial", t => t.Superior_CodigoSap)
                .Index(t => t.EstruturaComercialPapelID)
                .Index(t => t.Superior_CodigoSap);
            
            CreateTable(
                "dbo.EstruturaComercialPapel",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 1, fixedLength: true, unicode: false),
                        Papel = c.String(nullable: false, maxLength: 120, unicode: false),
                        Ativo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.GrupoEconomico",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Nome = c.String(nullable: false, maxLength: 120, unicode: false),
                        Descricao = c.String(nullable: false, maxLength: 120, unicode: false),
                        CodigoPrincipal = c.String(nullable: false, maxLength: 10, unicode: false),
                        Tipo = c.String(nullable: false, maxLength: 120, unicode: false),
                        Ativo = c.Boolean(nullable: false),
                        UsuarioIDCriacao = c.Guid(nullable: false),
                        UsuarioIDAlteracao = c.Guid(),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Representante",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        CodigoSap = c.String(nullable: false, maxLength: 10, unicode: false),
                        Nome = c.String(nullable: false, maxLength: 120, unicode: false),
                        Ativo = c.Boolean(nullable: false),
                        UsuarioIDCriacao = c.Guid(nullable: false),
                        UsuarioIDAlteracao = c.Guid(),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Usuario",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Nome = c.String(nullable: false, maxLength: 120, unicode: false),
                        Login = c.String(nullable: false, maxLength: 120, unicode: false),
                        TipoAcesso = c.Int(nullable: false),
                        Email = c.String(nullable: false, maxLength: 120, unicode: false),
                        Ativo = c.Boolean(nullable: false),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Grupo",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Nome = c.String(nullable: false, maxLength: 120, unicode: false),
                        Ativo = c.Boolean(nullable: false),
                        UsuarioIDCriacao = c.Guid(nullable: false),
                        UsuarioIDAlteracao = c.Guid(),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Permissao",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Nome = c.String(maxLength: 120, unicode: false),
                        Descricao = c.String(maxLength: 120, unicode: false),
                        Ativo = c.Boolean(nullable: false),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Segmento",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Descricao = c.String(nullable: false, maxLength: 120, unicode: false),
                        Ativo = c.Boolean(nullable: false),
                        UsuarioIDCriacao = c.Guid(nullable: false),
                        UsuarioIDAlteracao = c.Guid(),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.TipoCliente",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Nome = c.String(nullable: false, maxLength: 120, unicode: false),
                        Descricao = c.String(maxLength: 120, unicode: false),
                        Ativo = c.Boolean(nullable: false),
                        UsuarioIDCriacao = c.Guid(nullable: false),
                        UsuarioIDAlteracao = c.Guid(),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ContaClienteCodigo",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        ContaClienteID = c.Guid(nullable: false),
                        CodigoPrincipal = c.Boolean(nullable: false),
                        Codigo = c.String(nullable: false, maxLength: 10, unicode: false),
                        Documento = c.String(nullable: false, maxLength: 24, unicode: false),
                        UsuarioIDCriacao = c.Guid(nullable: false),
                        UsuarioIDAlteracao = c.Guid(),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ContaCliente", t => t.ContaClienteID)
                .Index(t => t.ContaClienteID);
            
            CreateTable(
                "dbo.ContaClienteComentario",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        ContaClienteID = c.Guid(nullable: false),
                        UsuarioID = c.Guid(nullable: false),
                        Descricao = c.String(nullable: false, maxLength: 255, unicode: false),
                        Ativo = c.Boolean(nullable: false),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ContaCliente", t => t.ContaClienteID)
                .ForeignKey("dbo.Usuario", t => t.UsuarioID)
                .Index(t => t.ContaClienteID)
                .Index(t => t.UsuarioID);
            
            CreateTable(
                "dbo.ContaClienteFinanceiro",
                c => new
                    {
                        ContaClienteID = c.Guid(nullable: false),
                        UsuarioIDCriacao = c.Guid(nullable: false),
                        UsuarioIDAlteracao = c.Guid(),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(),
                        LC = c.Single(nullable: false),
                        LCDisponivel = c.Single(nullable: false),
                        Exposicao = c.Single(nullable: false),
                        Vigencia = c.DateTime(nullable: false),
                        Rating = c.String(maxLength: 120, unicode: false),
                        ConceitoCobrancaID = c.Guid(nullable: false),
                        Recebiveis = c.Single(nullable: false),
                        OperacaoFinanciamento = c.Single(nullable: false),
                        ClientePremium = c.Boolean(nullable: false),
                        ContaCliente_ID = c.Guid(),
                    })
                .PrimaryKey(t => t.ContaClienteID)
                .ForeignKey("dbo.ConceitoCobranca", t => t.ConceitoCobrancaID)
                .ForeignKey("dbo.ContaCliente", t => t.ContaCliente_ID)
                .Index(t => t.ConceitoCobrancaID)
                .Index(t => t.ContaCliente_ID);
            
            CreateTable(
                "dbo.ContaClienteTelefone",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        ContaClienteID = c.Guid(nullable: false),
                        Tipo = c.Int(nullable: false),
                        Telefone = c.String(nullable: false, maxLength: 24, unicode: false),
                        Ativo = c.Boolean(nullable: false),
                        UsuarioIDCriacao = c.Guid(nullable: false),
                        UsuarioIDAlteracao = c.Guid(),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ContaCliente", t => t.ContaClienteID)
                .Index(t => t.ContaClienteID);
            
            CreateTable(
                "dbo.Cultura",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Tipo = c.String(nullable: false, maxLength: 50, unicode: false),
                        Ativo = c.Boolean(nullable: false),
                        UsuarioIDCriacao = c.Guid(nullable: false),
                        UsuarioIDAlteracao = c.Guid(),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.CustoHaRegiao",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Custo = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Descricao = c.String(nullable: false, maxLength: 240, unicode: false),
                        RegiaoID = c.Guid(nullable: false),
                        Ativo = c.Boolean(nullable: false),
                        UsuarioIDCriacao = c.Guid(nullable: false),
                        UsuarioIDAlteracao = c.Guid(),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Regiao", t => t.RegiaoID)
                .Index(t => t.RegiaoID);
            
            CreateTable(
                "dbo.Regiao",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Nome = c.String(nullable: false, maxLength: 240, unicode: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Experiencia",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Descricao = c.String(nullable: false, maxLength: 50, unicode: false),
                        Ativo = c.Boolean(nullable: false),
                        UsuarioIDCriacao = c.Guid(nullable: false),
                        UsuarioIDAlteracao = c.Guid(),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.IdadeMediaCanavial",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Nome = c.String(nullable: false, maxLength: 120, unicode: false),
                        Ativo = c.Boolean(nullable: false),
                        UsuarioIDCriacao = c.Guid(nullable: false),
                        UsuarioIDAlteracao = c.Guid(),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.LogLevel",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Status = c.String(maxLength: 120, unicode: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Log",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        LogLevelID = c.Int(nullable: false),
                        Usuario = c.String(maxLength: 120, unicode: false),
                        UsuarioID = c.Guid(nullable: false),
                        Descricao = c.String(maxLength: 255, unicode: false),
                        Pagina = c.String(maxLength: 120, unicode: false),
                        Navegador = c.String(maxLength: 255, unicode: false),
                        IP = c.String(maxLength: 120, unicode: false),
                        IDTransacao = c.Guid(),
                        Idioma = c.String(maxLength: 120, unicode: false),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.LogLevel", t => t.LogLevelID)
                .Index(t => t.LogLevelID);
            
            CreateTable(
                "dbo.ValorSaca",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Nome = c.String(nullable: false, maxLength: 100, unicode: false),
                        Peso = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Valor = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Ativo = c.Boolean(nullable: false),
                        UsuarioIDCriacao = c.Guid(nullable: false),
                        UsuarioIDAlteracao = c.Guid(),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Pais",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        NomePtbr = c.String(nullable: false, maxLength: 120, unicode: false),
                        NomeEnus = c.String(nullable: false, maxLength: 120, unicode: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.PorcentagemQuebra",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Porcentagem = c.Int(nullable: false),
                        Ativo = c.Boolean(nullable: false),
                        UsuarioIDCriacao = c.Guid(nullable: false),
                        UsuarioIDAlteracao = c.Guid(),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ProdutividadeMedia",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Nome = c.String(maxLength: 120, unicode: false),
                        RegiaoID = c.Guid(nullable: false),
                        Ativo = c.Boolean(nullable: false),
                        UsuarioIDCriacao = c.Guid(nullable: false),
                        UsuarioIDAlteracao = c.Guid(),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Regiao", t => t.RegiaoID)
                .Index(t => t.RegiaoID);
            
            CreateTable(
                "dbo.ProdutoServico",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Nome = c.String(nullable: false, maxLength: 120, unicode: false),
                        Ativo = c.Boolean(nullable: false),
                        UsuarioIDCriacao = c.Guid(nullable: false),
                        UsuarioIDAlteracao = c.Guid(),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Receita",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Descricao = c.String(nullable: false, maxLength: 50, unicode: false),
                        Ativo = c.Boolean(nullable: false),
                        UsuarioIDCriacao = c.Guid(nullable: false),
                        UsuarioIDAlteracao = c.Guid(),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.TipoEmpresa",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Tipo = c.String(nullable: false, maxLength: 50, unicode: false),
                        Ativo = c.Boolean(nullable: false),
                        UsuarioIDCriacao = c.Guid(nullable: false),
                        UsuarioIDAlteracao = c.Guid(),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.TipoGarantia",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Nome = c.String(nullable: false, maxLength: 120, unicode: false),
                        Ativo = c.Boolean(nullable: false),
                        UsuarioIDCriacao = c.Guid(nullable: false),
                        UsuarioIDAlteracao = c.Guid(),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.UnidadeMedidaCultura",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Nome = c.String(nullable: false, maxLength: 120, unicode: false),
                        Sigla = c.String(nullable: false, maxLength: 10, unicode: false),
                        Ativo = c.Boolean(nullable: false),
                        UsuarioIDCriacao = c.Guid(nullable: false),
                        UsuarioIDAlteracao = c.Guid(),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ContaCliente_EstruturaComercial",
                c => new
                    {
                        ContaClienteID = c.Guid(nullable: false),
                        EstruturaComercialID = c.String(nullable: false, maxLength: 10, unicode: false),
                    })
                .PrimaryKey(t => new { t.ContaClienteID, t.EstruturaComercialID })
                .ForeignKey("dbo.ContaCliente", t => t.ContaClienteID)
                .ForeignKey("dbo.EstruturaComercial", t => t.EstruturaComercialID)
                .Index(t => t.ContaClienteID)
                .Index(t => t.EstruturaComercialID);
            
            CreateTable(
                "dbo.ContaCliente_GrupoEconomico",
                c => new
                    {
                        ContaClienteID = c.Guid(nullable: false),
                        GrupoEconomicoID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.ContaClienteID, t.GrupoEconomicoID })
                .ForeignKey("dbo.ContaCliente", t => t.ContaClienteID)
                .ForeignKey("dbo.GrupoEconomico", t => t.GrupoEconomicoID)
                .Index(t => t.ContaClienteID)
                .Index(t => t.GrupoEconomicoID);
            
            CreateTable(
                "dbo.Grupo_Permissao",
                c => new
                    {
                        GrupoID = c.Guid(nullable: false),
                        PermissaoID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.GrupoID, t.PermissaoID })
                .ForeignKey("dbo.Grupo", t => t.GrupoID)
                .ForeignKey("dbo.Permissao", t => t.PermissaoID)
                .Index(t => t.GrupoID)
                .Index(t => t.PermissaoID);
            
            CreateTable(
                "dbo.Usuario_Grupo",
                c => new
                    {
                        UsuarioID = c.Guid(nullable: false),
                        GrupoID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.UsuarioID, t.GrupoID })
                .ForeignKey("dbo.Usuario", t => t.UsuarioID)
                .ForeignKey("dbo.Grupo", t => t.GrupoID)
                .Index(t => t.UsuarioID)
                .Index(t => t.GrupoID);
            
            CreateTable(
                "dbo.Usuario_Representante",
                c => new
                    {
                        UsuarioID = c.Guid(nullable: false),
                        RepresentanteID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.UsuarioID, t.RepresentanteID })
                .ForeignKey("dbo.Usuario", t => t.UsuarioID)
                .ForeignKey("dbo.Representante", t => t.RepresentanteID)
                .Index(t => t.UsuarioID)
                .Index(t => t.RepresentanteID);
            
            CreateTable(
                "dbo.ContaCliente_Representantes",
                c => new
                    {
                        ContaClienteID = c.Guid(nullable: false),
                        RepresentanteID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.ContaClienteID, t.RepresentanteID })
                .ForeignKey("dbo.ContaCliente", t => t.ContaClienteID)
                .ForeignKey("dbo.Representante", t => t.RepresentanteID)
                .Index(t => t.ContaClienteID)
                .Index(t => t.RepresentanteID);
            
            CreateTable(
                "dbo.ContaCliente_Segmento",
                c => new
                    {
                        ContaClienteID = c.Guid(nullable: false),
                        SegmentoID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.ContaClienteID, t.SegmentoID })
                .ForeignKey("dbo.ContaCliente", t => t.ContaClienteID)
                .ForeignKey("dbo.Segmento", t => t.SegmentoID)
                .Index(t => t.ContaClienteID)
                .Index(t => t.SegmentoID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProdutividadeMedia", "RegiaoID", "dbo.Regiao");
            DropForeignKey("dbo.Log", "LogLevelID", "dbo.LogLevel");
            DropForeignKey("dbo.CustoHaRegiao", "RegiaoID", "dbo.Regiao");
            DropForeignKey("dbo.ContaClienteTelefone", "ContaClienteID", "dbo.ContaCliente");
            DropForeignKey("dbo.ContaClienteFinanceiro", "ContaCliente_ID", "dbo.ContaCliente");
            DropForeignKey("dbo.ContaClienteFinanceiro", "ConceitoCobrancaID", "dbo.ConceitoCobranca");
            DropForeignKey("dbo.ContaClienteComentario", "UsuarioID", "dbo.Usuario");
            DropForeignKey("dbo.ContaClienteComentario", "ContaClienteID", "dbo.ContaCliente");
            DropForeignKey("dbo.ContaClienteCodigo", "ContaClienteID", "dbo.ContaCliente");
            DropForeignKey("dbo.ContaCliente", "TipoClienteID", "dbo.TipoCliente");
            DropForeignKey("dbo.ContaCliente_Segmento", "SegmentoID", "dbo.Segmento");
            DropForeignKey("dbo.ContaCliente_Segmento", "ContaClienteID", "dbo.ContaCliente");
            DropForeignKey("dbo.ContaCliente_Representantes", "RepresentanteID", "dbo.Representante");
            DropForeignKey("dbo.ContaCliente_Representantes", "ContaClienteID", "dbo.ContaCliente");
            DropForeignKey("dbo.Usuario_Representante", "RepresentanteID", "dbo.Representante");
            DropForeignKey("dbo.Usuario_Representante", "UsuarioID", "dbo.Usuario");
            DropForeignKey("dbo.Usuario_Grupo", "GrupoID", "dbo.Grupo");
            DropForeignKey("dbo.Usuario_Grupo", "UsuarioID", "dbo.Usuario");
            DropForeignKey("dbo.Grupo_Permissao", "PermissaoID", "dbo.Permissao");
            DropForeignKey("dbo.Grupo_Permissao", "GrupoID", "dbo.Grupo");
            DropForeignKey("dbo.ContaCliente_GrupoEconomico", "GrupoEconomicoID", "dbo.GrupoEconomico");
            DropForeignKey("dbo.ContaCliente_GrupoEconomico", "ContaClienteID", "dbo.ContaCliente");
            DropForeignKey("dbo.ContaCliente_EstruturaComercial", "EstruturaComercialID", "dbo.EstruturaComercial");
            DropForeignKey("dbo.ContaCliente_EstruturaComercial", "ContaClienteID", "dbo.ContaCliente");
            DropForeignKey("dbo.EstruturaComercial", "Superior_CodigoSap", "dbo.EstruturaComercial");
            DropForeignKey("dbo.EstruturaComercial", "EstruturaComercialPapelID", "dbo.EstruturaComercialPapel");
            DropIndex("dbo.ContaCliente_Segmento", new[] { "SegmentoID" });
            DropIndex("dbo.ContaCliente_Segmento", new[] { "ContaClienteID" });
            DropIndex("dbo.ContaCliente_Representantes", new[] { "RepresentanteID" });
            DropIndex("dbo.ContaCliente_Representantes", new[] { "ContaClienteID" });
            DropIndex("dbo.Usuario_Representante", new[] { "RepresentanteID" });
            DropIndex("dbo.Usuario_Representante", new[] { "UsuarioID" });
            DropIndex("dbo.Usuario_Grupo", new[] { "GrupoID" });
            DropIndex("dbo.Usuario_Grupo", new[] { "UsuarioID" });
            DropIndex("dbo.Grupo_Permissao", new[] { "PermissaoID" });
            DropIndex("dbo.Grupo_Permissao", new[] { "GrupoID" });
            DropIndex("dbo.ContaCliente_GrupoEconomico", new[] { "GrupoEconomicoID" });
            DropIndex("dbo.ContaCliente_GrupoEconomico", new[] { "ContaClienteID" });
            DropIndex("dbo.ContaCliente_EstruturaComercial", new[] { "EstruturaComercialID" });
            DropIndex("dbo.ContaCliente_EstruturaComercial", new[] { "ContaClienteID" });
            DropIndex("dbo.ProdutividadeMedia", new[] { "RegiaoID" });
            DropIndex("dbo.Log", new[] { "LogLevelID" });
            DropIndex("dbo.CustoHaRegiao", new[] { "RegiaoID" });
            DropIndex("dbo.ContaClienteTelefone", new[] { "ContaClienteID" });
            DropIndex("dbo.ContaClienteFinanceiro", new[] { "ContaCliente_ID" });
            DropIndex("dbo.ContaClienteFinanceiro", new[] { "ConceitoCobrancaID" });
            DropIndex("dbo.ContaClienteComentario", new[] { "UsuarioID" });
            DropIndex("dbo.ContaClienteComentario", new[] { "ContaClienteID" });
            DropIndex("dbo.ContaClienteCodigo", new[] { "ContaClienteID" });
            DropIndex("dbo.EstruturaComercial", new[] { "Superior_CodigoSap" });
            DropIndex("dbo.EstruturaComercial", new[] { "EstruturaComercialPapelID" });
            DropIndex("dbo.ContaCliente", new[] { "TipoClienteID" });
            DropIndex("dbo.ConceitoCobranca", "Index");
            DropTable("dbo.ContaCliente_Segmento");
            DropTable("dbo.ContaCliente_Representantes");
            DropTable("dbo.Usuario_Representante");
            DropTable("dbo.Usuario_Grupo");
            DropTable("dbo.Grupo_Permissao");
            DropTable("dbo.ContaCliente_GrupoEconomico");
            DropTable("dbo.ContaCliente_EstruturaComercial");
            DropTable("dbo.UnidadeMedidaCultura");
            DropTable("dbo.TipoGarantia");
            DropTable("dbo.TipoEmpresa");
            DropTable("dbo.Receita");
            DropTable("dbo.ProdutoServico");
            DropTable("dbo.ProdutividadeMedia");
            DropTable("dbo.PorcentagemQuebra");
            DropTable("dbo.Pais");
            DropTable("dbo.ValorSaca");
            DropTable("dbo.Log");
            DropTable("dbo.LogLevel");
            DropTable("dbo.IdadeMediaCanavial");
            DropTable("dbo.Experiencia");
            DropTable("dbo.Regiao");
            DropTable("dbo.CustoHaRegiao");
            DropTable("dbo.Cultura");
            DropTable("dbo.ContaClienteTelefone");
            DropTable("dbo.ContaClienteFinanceiro");
            DropTable("dbo.ContaClienteComentario");
            DropTable("dbo.ContaClienteCodigo");
            DropTable("dbo.TipoCliente");
            DropTable("dbo.Segmento");
            DropTable("dbo.Permissao");
            DropTable("dbo.Grupo");
            DropTable("dbo.Usuario");
            DropTable("dbo.Representante");
            DropTable("dbo.GrupoEconomico");
            DropTable("dbo.EstruturaComercialPapel");
            DropTable("dbo.EstruturaComercial");
            DropTable("dbo.ContaCliente");
            DropTable("dbo.ConceitoCobranca");
            DropTable("dbo.AreaIrrigada");
        }
    }
}
