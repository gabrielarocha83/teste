namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PropostaLCAdicionalComiteSolicitante_Estrutura : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SolicitanteFluxoComiteAdicional",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        UsuarioID = c.Guid(nullable: false),
                        DataCriacao = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Usuario", t => t.UsuarioID)
                .Index(t => t.UsuarioID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SolicitanteFluxoComiteAdicional", "UsuarioID", "dbo.Usuario");
            DropIndex("dbo.SolicitanteFluxoComiteAdicional", new[] { "UsuarioID" });
            DropTable("dbo.SolicitanteFluxoComiteAdicional");
        }
    }
}
