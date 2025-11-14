namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PropostaLCAdicionalHistorico_Estrutura : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PropostaLCAdicionalHistorico",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        DataCriacao = c.DateTime(nullable: false),
                        PropostaLCAdicionalID = c.Guid(nullable: false),
                        PropostaLCStatusID = c.String(nullable: false, maxLength: 2, unicode: false),
                        UsuarioID = c.Guid(nullable: false),
                        Descricao = c.String(maxLength: 120, unicode: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.PropostaLCAdicional", t => t.PropostaLCAdicionalID)
                .ForeignKey("dbo.PropostaLCStatus", t => t.PropostaLCStatusID)
                .ForeignKey("dbo.Usuario", t => t.UsuarioID)
                .Index(t => t.PropostaLCAdicionalID)
                .Index(t => t.PropostaLCStatusID)
                .Index(t => t.UsuarioID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PropostaLCAdicionalHistorico", "UsuarioID", "dbo.Usuario");
            DropForeignKey("dbo.PropostaLCAdicionalHistorico", "PropostaLCStatusID", "dbo.PropostaLCStatus");
            DropForeignKey("dbo.PropostaLCAdicionalHistorico", "PropostaLCAdicionalID", "dbo.PropostaLCAdicional");
            DropIndex("dbo.PropostaLCAdicionalHistorico", new[] { "UsuarioID" });
            DropIndex("dbo.PropostaLCAdicionalHistorico", new[] { "PropostaLCStatusID" });
            DropIndex("dbo.PropostaLCAdicionalHistorico", new[] { "PropostaLCAdicionalID" });
            DropTable("dbo.PropostaLCAdicionalHistorico");
        }
    }
}
