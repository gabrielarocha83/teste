namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlcadaComercialEstrutura : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PropostaAlcadaComercialCultura",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        PropostaAlcadaComercialID = c.Guid(nullable: false),
                        CulturaID = c.Guid(),
                        EstadoID = c.Guid(),
                        CidadeID = c.Guid(),
                        Documento = c.String(maxLength: 120, unicode: false),
                        AreaPropria = c.Decimal(precision: 18, scale: 2),
                        AreaArrendada = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Cidade", t => t.CidadeID)
                .ForeignKey("dbo.Cultura", t => t.CulturaID)
                .ForeignKey("dbo.Estado", t => t.EstadoID)
                .ForeignKey("dbo.PropostaAlcadaComercial", t => t.PropostaAlcadaComercialID)
                .Index(t => t.PropostaAlcadaComercialID)
                .Index(t => t.CulturaID)
                .Index(t => t.EstadoID)
                .Index(t => t.CidadeID);
            
            CreateTable(
                "dbo.PropostaAlcadaComercial",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        TipoClienteID = c.Guid(),
                        ContaClienteID = c.Guid(nullable: false),
                        ExperienciaID = c.Guid(),
                        PorteCliente = c.String(maxLength: 120, unicode: false),
                        FaturamentoAnual = c.Decimal(precision: 18, scale: 2),
                        CTC = c.String(maxLength: 120, unicode: false),
                        GC = c.String(maxLength: 120, unicode: false),
                        TermoAceite = c.Boolean(nullable: false),
                        CodigoSap = c.String(maxLength: 10, unicode: false),
                        EstadoCivil = c.String(maxLength: 120, unicode: false),
                        CPFConjugue = c.String(maxLength: 120, unicode: false),
                        NomeConjugue = c.String(maxLength: 120, unicode: false),
                        RegimeCasamento = c.String(maxLength: 120, unicode: false),
                        LCProposto = c.Decimal(precision: 18, scale: 2),
                        SharePretendido = c.Decimal(precision: 18, scale: 2),
                        PrazoDias = c.Int(),
                        FontePagamento = c.String(maxLength: 120, unicode: false),
                        ParecerRepresentante = c.String(unicode: false, storeType: "text"),
                        ParecerCTC = c.String(unicode: false, storeType: "text"),
                        ParecerCredito = c.String(unicode: false, storeType: "text"),
                        RestricaoSerasa = c.Boolean(nullable: false),
                        ResponsavelID = c.Guid(nullable: false),
                        SolicitanteSerasaPropostaID = c.Guid(),
                        DataFundacao = c.DateTime(nullable: false),
                        SolicitanteSerasaConjugeID = c.Guid(),
                        DetalheRestricaoSerasa = c.String(maxLength: 120, unicode: false),
                        PropostaCobrancaStatusID = c.String(maxLength: 2, fixedLength: true, unicode: false),
                        EmpresaID = c.String(maxLength: 1, fixedLength: true, unicode: false),
                        UsuarioIDCriacao = c.Guid(nullable: false),
                        UsuarioIDAlteracao = c.Guid(),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ContaCliente", t => t.ContaClienteID)
                .ForeignKey("dbo.Empresa", t => t.EmpresaID)
                .ForeignKey("dbo.Experiencia", t => t.ExperienciaID)
                .ForeignKey("dbo.PropostaCobrancaStatus", t => t.PropostaCobrancaStatusID)
                .ForeignKey("dbo.Usuario", t => t.ResponsavelID)
                .ForeignKey("dbo.SolicitanteSerasa", t => t.SolicitanteSerasaConjugeID)
                .ForeignKey("dbo.SolicitanteSerasa", t => t.SolicitanteSerasaPropostaID)
                .ForeignKey("dbo.TipoCliente", t => t.TipoClienteID)
                .Index(t => t.TipoClienteID)
                .Index(t => t.ContaClienteID)
                .Index(t => t.ExperienciaID)
                .Index(t => t.ResponsavelID)
                .Index(t => t.SolicitanteSerasaPropostaID)
                .Index(t => t.SolicitanteSerasaConjugeID)
                .Index(t => t.PropostaCobrancaStatusID)
                .Index(t => t.EmpresaID);
            
            CreateTable(
                "dbo.PropostaAlcadaComercialParceriaAgricola",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        PropostaAlcadaComercialID = c.Guid(nullable: false),
                        Documento = c.String(maxLength: 120, unicode: false),
                        InscricaoEstadual = c.String(maxLength: 120, unicode: false),
                        Nome = c.String(maxLength: 120, unicode: false),
                        SolicitanteSerasaID = c.Guid(),
                        RestricaoSerasa = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.PropostaAlcadaComercial", t => t.PropostaAlcadaComercialID)
                .ForeignKey("dbo.SolicitanteSerasa", t => t.SolicitanteSerasaID)
                .Index(t => t.PropostaAlcadaComercialID)
                .Index(t => t.SolicitanteSerasaID);
            
            CreateTable(
                "dbo.PropostaAlcadaComercialProdutoServico",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        ProdutoServicoID = c.Guid(),
                        PropostaAlcadaComercialID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ProdutoServico", t => t.ProdutoServicoID)
                .ForeignKey("dbo.PropostaAlcadaComercial", t => t.PropostaAlcadaComercialID)
                .Index(t => t.ProdutoServicoID)
                .Index(t => t.PropostaAlcadaComercialID);
            
            CreateTable(
                "dbo.PropostaAlcadaComercialDocumento",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Documento = c.String(maxLength: 120, unicode: false),
                        RestricaoSerasa = c.Boolean(nullable: false),
                        PropostaAlcadaComercialID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.PropostaAlcadaComercial", t => t.PropostaAlcadaComercialID)
                .Index(t => t.PropostaAlcadaComercialID);
            
            CreateTable(
                "dbo.PropostaAlcadaComercialRestricao",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        ContaClienteID = c.Guid(nullable: false),
                        Mensagem = c.String(maxLength: 120, unicode: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PropostaAlcadaComercialDocumento", "PropostaAlcadaComercialID", "dbo.PropostaAlcadaComercial");
            DropForeignKey("dbo.PropostaAlcadaComercial", "TipoClienteID", "dbo.TipoCliente");
            DropForeignKey("dbo.PropostaAlcadaComercial", "SolicitanteSerasaPropostaID", "dbo.SolicitanteSerasa");
            DropForeignKey("dbo.PropostaAlcadaComercial", "SolicitanteSerasaConjugeID", "dbo.SolicitanteSerasa");
            DropForeignKey("dbo.PropostaAlcadaComercial", "ResponsavelID", "dbo.Usuario");
            DropForeignKey("dbo.PropostaAlcadaComercial", "PropostaCobrancaStatusID", "dbo.PropostaCobrancaStatus");
            DropForeignKey("dbo.PropostaAlcadaComercialProdutoServico", "PropostaAlcadaComercialID", "dbo.PropostaAlcadaComercial");
            DropForeignKey("dbo.PropostaAlcadaComercialProdutoServico", "ProdutoServicoID", "dbo.ProdutoServico");
            DropForeignKey("dbo.PropostaAlcadaComercialParceriaAgricola", "SolicitanteSerasaID", "dbo.SolicitanteSerasa");
            DropForeignKey("dbo.PropostaAlcadaComercialParceriaAgricola", "PropostaAlcadaComercialID", "dbo.PropostaAlcadaComercial");
            DropForeignKey("dbo.PropostaAlcadaComercial", "ExperienciaID", "dbo.Experiencia");
            DropForeignKey("dbo.PropostaAlcadaComercial", "EmpresaID", "dbo.Empresa");
            DropForeignKey("dbo.PropostaAlcadaComercialCultura", "PropostaAlcadaComercialID", "dbo.PropostaAlcadaComercial");
            DropForeignKey("dbo.PropostaAlcadaComercial", "ContaClienteID", "dbo.ContaCliente");
            DropForeignKey("dbo.PropostaAlcadaComercialCultura", "EstadoID", "dbo.Estado");
            DropForeignKey("dbo.PropostaAlcadaComercialCultura", "CulturaID", "dbo.Cultura");
            DropForeignKey("dbo.PropostaAlcadaComercialCultura", "CidadeID", "dbo.Cidade");
            DropIndex("dbo.PropostaAlcadaComercialDocumento", new[] { "PropostaAlcadaComercialID" });
            DropIndex("dbo.PropostaAlcadaComercialProdutoServico", new[] { "PropostaAlcadaComercialID" });
            DropIndex("dbo.PropostaAlcadaComercialProdutoServico", new[] { "ProdutoServicoID" });
            DropIndex("dbo.PropostaAlcadaComercialParceriaAgricola", new[] { "SolicitanteSerasaID" });
            DropIndex("dbo.PropostaAlcadaComercialParceriaAgricola", new[] { "PropostaAlcadaComercialID" });
            DropIndex("dbo.PropostaAlcadaComercial", new[] { "EmpresaID" });
            DropIndex("dbo.PropostaAlcadaComercial", new[] { "PropostaCobrancaStatusID" });
            DropIndex("dbo.PropostaAlcadaComercial", new[] { "SolicitanteSerasaConjugeID" });
            DropIndex("dbo.PropostaAlcadaComercial", new[] { "SolicitanteSerasaPropostaID" });
            DropIndex("dbo.PropostaAlcadaComercial", new[] { "ResponsavelID" });
            DropIndex("dbo.PropostaAlcadaComercial", new[] { "ExperienciaID" });
            DropIndex("dbo.PropostaAlcadaComercial", new[] { "ContaClienteID" });
            DropIndex("dbo.PropostaAlcadaComercial", new[] { "TipoClienteID" });
            DropIndex("dbo.PropostaAlcadaComercialCultura", new[] { "CidadeID" });
            DropIndex("dbo.PropostaAlcadaComercialCultura", new[] { "EstadoID" });
            DropIndex("dbo.PropostaAlcadaComercialCultura", new[] { "CulturaID" });
            DropIndex("dbo.PropostaAlcadaComercialCultura", new[] { "PropostaAlcadaComercialID" });
            DropTable("dbo.PropostaAlcadaComercialRestricao");
            DropTable("dbo.PropostaAlcadaComercialDocumento");
            DropTable("dbo.PropostaAlcadaComercialProdutoServico");
            DropTable("dbo.PropostaAlcadaComercialParceriaAgricola");
            DropTable("dbo.PropostaAlcadaComercial");
            DropTable("dbo.PropostaAlcadaComercialCultura");
        }
    }
}
