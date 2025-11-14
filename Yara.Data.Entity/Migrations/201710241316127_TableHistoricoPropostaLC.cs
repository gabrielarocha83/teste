namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TableHistoricoPropostaLC : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PropostaLCHistorico",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        DataCriacao = c.DateTime(nullable: false),
                        UsuarioID = c.Guid(nullable: false),
                        PropostaLCID = c.Guid(nullable: false),
                        PropostaLCStatusID = c.String(nullable: false, maxLength: 2, unicode: false),
                        Descricao = c.String(maxLength: 120, unicode: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.PropostaLC", t => t.PropostaLCID)
                .ForeignKey("dbo.PropostaLCStatus", t => t.PropostaLCStatusID)
                .ForeignKey("dbo.Usuario", t => t.UsuarioID)
                .Index(t => t.UsuarioID)
                .Index(t => t.PropostaLCID)
                .Index(t => t.PropostaLCStatusID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PropostaLCHistorico", "UsuarioID", "dbo.Usuario");
            DropForeignKey("dbo.PropostaLCHistorico", "PropostaLCStatusID", "dbo.PropostaLCStatus");
            DropForeignKey("dbo.PropostaLCHistorico", "PropostaLCID", "dbo.PropostaLC");
            DropIndex("dbo.PropostaLCHistorico", new[] { "PropostaLCStatusID" });
            DropIndex("dbo.PropostaLCHistorico", new[] { "PropostaLCID" });
            DropIndex("dbo.PropostaLCHistorico", new[] { "UsuarioID" });
            DropTable("dbo.PropostaLCHistorico");
        }
    }
}
