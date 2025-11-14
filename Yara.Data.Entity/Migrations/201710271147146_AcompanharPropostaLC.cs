namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AcompanharPropostaLC : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PropostaLCAcompanhamento",
                c => new
                    {
                        PropostaLCID = c.Guid(nullable: false),
                        UsuarioID = c.Guid(nullable: false),
                        DataCriacao = c.DateTime(nullable: false),
                        Ativo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.PropostaLCID, t.UsuarioID })
                .ForeignKey("dbo.PropostaLC", t => t.PropostaLCID)
                .ForeignKey("dbo.Usuario", t => t.UsuarioID)
                .Index(t => t.PropostaLCID)
                .Index(t => t.UsuarioID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PropostaLCAcompanhamento", "UsuarioID", "dbo.Usuario");
            DropForeignKey("dbo.PropostaLCAcompanhamento", "PropostaLCID", "dbo.PropostaLC");
            DropIndex("dbo.PropostaLCAcompanhamento", new[] { "UsuarioID" });
            DropIndex("dbo.PropostaLCAcompanhamento", new[] { "PropostaLCID" });
            DropTable("dbo.PropostaLCAcompanhamento");
        }
    }
}
