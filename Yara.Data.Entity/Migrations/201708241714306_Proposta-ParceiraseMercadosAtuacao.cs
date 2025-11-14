namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PropostaParceiraseMercadosAtuacao : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PropostaLCParceriaAgricula",
                c => new
                    {
                        ContaClienteID = c.Guid(nullable: false),
                        PropostaLCID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.ContaClienteID, t.PropostaLCID })
                .ForeignKey("dbo.PropostaLC", t => t.ContaClienteID)
                .ForeignKey("dbo.ContaCliente", t => t.PropostaLCID)
                .Index(t => t.ContaClienteID)
                .Index(t => t.PropostaLCID);
            
            CreateTable(
                "dbo.PropostaLCPrincipaisMercadosAtuacao",
                c => new
                    {
                        CulturaID = c.Guid(nullable: false),
                        PropostaLCID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.CulturaID, t.PropostaLCID })
                .ForeignKey("dbo.PropostaLC", t => t.CulturaID)
                .ForeignKey("dbo.Cultura", t => t.PropostaLCID)
                .Index(t => t.CulturaID)
                .Index(t => t.PropostaLCID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PropostaLCPrincipaisMercadosAtuacao", "PropostaLCID", "dbo.Cultura");
            DropForeignKey("dbo.PropostaLCPrincipaisMercadosAtuacao", "CulturaID", "dbo.PropostaLC");
            DropForeignKey("dbo.PropostaLCParceriaAgricula", "PropostaLCID", "dbo.ContaCliente");
            DropForeignKey("dbo.PropostaLCParceriaAgricula", "ContaClienteID", "dbo.PropostaLC");
            DropIndex("dbo.PropostaLCPrincipaisMercadosAtuacao", new[] { "PropostaLCID" });
            DropIndex("dbo.PropostaLCPrincipaisMercadosAtuacao", new[] { "CulturaID" });
            DropIndex("dbo.PropostaLCParceriaAgricula", new[] { "PropostaLCID" });
            DropIndex("dbo.PropostaLCParceriaAgricula", new[] { "ContaClienteID" });
            DropTable("dbo.PropostaLCPrincipaisMercadosAtuacao");
            DropTable("dbo.PropostaLCParceriaAgricula");
        }
    }
}
