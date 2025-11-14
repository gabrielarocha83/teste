namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPropostaAlcadaComercialAcompanhamento : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PropostaAlcadaComercialAcompanhamento",
                c => new
                    {
                        PropostaAlcadaComercialID = c.Guid(nullable: false),
                        UsuarioID = c.Guid(nullable: false),
                        DataCriacao = c.DateTime(nullable: false),
                        Ativo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.PropostaAlcadaComercialID, t.UsuarioID })
                .ForeignKey("dbo.PropostaAlcadaComercial", t => t.PropostaAlcadaComercialID)
                .ForeignKey("dbo.Usuario", t => t.UsuarioID)
                .Index(t => t.PropostaAlcadaComercialID)
                .Index(t => t.UsuarioID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PropostaAlcadaComercialAcompanhamento", "UsuarioID", "dbo.Usuario");
            DropForeignKey("dbo.PropostaAlcadaComercialAcompanhamento", "PropostaAlcadaComercialID", "dbo.PropostaAlcadaComercial");
            DropIndex("dbo.PropostaAlcadaComercialAcompanhamento", new[] { "UsuarioID" });
            DropIndex("dbo.PropostaAlcadaComercialAcompanhamento", new[] { "PropostaAlcadaComercialID" });
            DropTable("dbo.PropostaAlcadaComercialAcompanhamento");
        }
    }
}
