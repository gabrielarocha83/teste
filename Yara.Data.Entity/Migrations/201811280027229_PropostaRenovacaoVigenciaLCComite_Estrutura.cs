namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PropostaRenovacaoVigenciaLCComite_Estrutura : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PropostaRenovacaoVigenciaLCComite",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        PropostaRenovacaoVigenciaLCID = c.Guid(nullable: false),
                        DataCriacao = c.DateTime(nullable: false),
                        Nivel = c.Int(nullable: false),
                        UsuarioID = c.Guid(nullable: false),
                        DataAcao = c.DateTime(),
                        Comentario = c.String(unicode: false, storeType: "text"),
                        StatusComiteID = c.String(nullable: false, maxLength: 2, unicode: false),
                        FluxoRenovacaoVigenciaLCID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.FluxoRenovacaoVigenciaLC", t => t.FluxoRenovacaoVigenciaLCID)
                .ForeignKey("dbo.PropostaRenovacaoVigenciaLC", t => t.PropostaRenovacaoVigenciaLCID)
                .ForeignKey("dbo.PropostaLCStatusComite", t => t.StatusComiteID)
                .ForeignKey("dbo.Usuario", t => t.UsuarioID)
                .Index(t => t.PropostaRenovacaoVigenciaLCID)
                .Index(t => t.UsuarioID)
                .Index(t => t.StatusComiteID)
                .Index(t => t.FluxoRenovacaoVigenciaLCID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PropostaRenovacaoVigenciaLCComite", "UsuarioID", "dbo.Usuario");
            DropForeignKey("dbo.PropostaRenovacaoVigenciaLCComite", "StatusComiteID", "dbo.PropostaLCStatusComite");
            DropForeignKey("dbo.PropostaRenovacaoVigenciaLCComite", "PropostaRenovacaoVigenciaLCID", "dbo.PropostaRenovacaoVigenciaLC");
            DropForeignKey("dbo.PropostaRenovacaoVigenciaLCComite", "FluxoRenovacaoVigenciaLCID", "dbo.FluxoRenovacaoVigenciaLC");
            DropIndex("dbo.PropostaRenovacaoVigenciaLCComite", new[] { "FluxoRenovacaoVigenciaLCID" });
            DropIndex("dbo.PropostaRenovacaoVigenciaLCComite", new[] { "StatusComiteID" });
            DropIndex("dbo.PropostaRenovacaoVigenciaLCComite", new[] { "UsuarioID" });
            DropIndex("dbo.PropostaRenovacaoVigenciaLCComite", new[] { "PropostaRenovacaoVigenciaLCID" });
            DropTable("dbo.PropostaRenovacaoVigenciaLCComite");
        }
    }
}
