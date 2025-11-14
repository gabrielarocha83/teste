namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddComiteTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PropostaLCComite",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        PropostaLCID = c.Guid(nullable: false),
                        DataCriacao = c.DateTime(nullable: false),
                        Nivel = c.Int(nullable: false),
                        Round = c.Int(nullable: false),
                        PerfilID = c.Guid(nullable: false),
                        UsuarioID = c.Guid(nullable: false),
                        DataAcao = c.DateTime(),
                        ValorDe = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ValorAte = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ValorEstipulado = c.Decimal(precision: 18, scale: 2),
                        Comentario = c.String(maxLength: 120, unicode: false),
                        StatusComiteID = c.String(nullable: false, maxLength: 2, unicode: false),
                        Ativo = c.Boolean(nullable: false),
                        Adicionado = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.ID, t.PropostaLCID })
                .ForeignKey("dbo.Perfil", t => t.PerfilID)
                .ForeignKey("dbo.PropostaLC", t => t.PropostaLCID)
                .ForeignKey("dbo.PropostaLCStatusComite", t => t.StatusComiteID)
                .ForeignKey("dbo.Usuario", t => t.UsuarioID)
                .Index(t => t.PropostaLCID)
                .Index(t => t.PerfilID)
                .Index(t => t.UsuarioID)
                .Index(t => t.StatusComiteID);
            
            CreateTable(
                "dbo.PropostaLCStatusComite",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 2, unicode: false),
                        Nome = c.String(maxLength: 120, unicode: false),
                        Ativo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PropostaLCComite", "UsuarioID", "dbo.Usuario");
            DropForeignKey("dbo.PropostaLCComite", "StatusComiteID", "dbo.PropostaLCStatusComite");
            DropForeignKey("dbo.PropostaLCComite", "PropostaLCID", "dbo.PropostaLC");
            DropForeignKey("dbo.PropostaLCComite", "PerfilID", "dbo.Perfil");
            DropIndex("dbo.PropostaLCComite", new[] { "StatusComiteID" });
            DropIndex("dbo.PropostaLCComite", new[] { "UsuarioID" });
            DropIndex("dbo.PropostaLCComite", new[] { "PerfilID" });
            DropIndex("dbo.PropostaLCComite", new[] { "PropostaLCID" });
            DropTable("dbo.PropostaLCStatusComite");
            DropTable("dbo.PropostaLCComite");
        }
    }
}
