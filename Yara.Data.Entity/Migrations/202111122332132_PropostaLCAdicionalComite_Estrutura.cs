namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PropostaLCAdicionalComite_Estrutura : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PropostaLCAdicionalComite",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        PropostaLCAdicionalID = c.Guid(nullable: false),
                        Nivel = c.Int(nullable: false),
                        Round = c.Int(nullable: false),
                        DataAcao = c.DateTime(),
                        ValorDe = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ValorAte = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ValorEstipulado = c.Decimal(precision: 18, scale: 2),
                        Comentario = c.String(unicode: false, storeType: "text"),
                        Adicionado = c.Boolean(nullable: false),
                        NivelFinal = c.Boolean(nullable: false),
                        EmpresaID = c.String(maxLength: 120, unicode: false),
                        CodigoSAP = c.String(maxLength: 120, unicode: false),
                        Ativo = c.Boolean(nullable: false),
                        PerfilID = c.Guid(nullable: false),
                        UsuarioID = c.Guid(nullable: false),
                        PropostaLCAdicionalComiteSolicitanteID = c.Guid(nullable: false),
                        FluxoLiberacaoLCAdicionalID = c.Guid(nullable: false),
                        PropostaLCAdicionalStatusComiteID = c.String(nullable: false, maxLength: 2, unicode: false),
                        UsuarioIDCriacao = c.Guid(nullable: false),
                        UsuarioIDAlteracao = c.Guid(),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(),
                    })
                .PrimaryKey(t => new { t.ID, t.PropostaLCAdicionalID })
                .ForeignKey("dbo.FluxoLiberacaoLCAdicional", t => t.FluxoLiberacaoLCAdicionalID)
                .ForeignKey("dbo.Perfil", t => t.PerfilID)
                .ForeignKey("dbo.PropostaLCAdicional", t => t.PropostaLCAdicionalID)
                .ForeignKey("dbo.SolicitanteFluxoComiteAdicional", t => t.PropostaLCAdicionalComiteSolicitanteID)
                .ForeignKey("dbo.PropostaLCAdicionalStatusComite", t => t.PropostaLCAdicionalStatusComiteID)
                .ForeignKey("dbo.Usuario", t => t.UsuarioID)
                .Index(t => t.PropostaLCAdicionalID)
                .Index(t => t.PerfilID)
                .Index(t => t.UsuarioID)
                .Index(t => t.PropostaLCAdicionalComiteSolicitanteID)
                .Index(t => t.FluxoLiberacaoLCAdicionalID)
                .Index(t => t.PropostaLCAdicionalStatusComiteID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PropostaLCAdicionalComite", "UsuarioID", "dbo.Usuario");
            DropForeignKey("dbo.PropostaLCAdicionalComite", "PropostaLCAdicionalStatusComiteID", "dbo.PropostaLCAdicionalStatusComite");
            DropForeignKey("dbo.PropostaLCAdicionalComite", "PropostaLCAdicionalComiteSolicitanteID", "dbo.SolicitanteFluxoComiteAdicional");
            DropForeignKey("dbo.PropostaLCAdicionalComite", "PropostaLCAdicionalID", "dbo.PropostaLCAdicional");
            DropForeignKey("dbo.PropostaLCAdicionalComite", "PerfilID", "dbo.Perfil");
            DropForeignKey("dbo.PropostaLCAdicionalComite", "FluxoLiberacaoLCAdicionalID", "dbo.FluxoLiberacaoLCAdicional");
            DropIndex("dbo.PropostaLCAdicionalComite", new[] { "PropostaLCAdicionalStatusComiteID" });
            DropIndex("dbo.PropostaLCAdicionalComite", new[] { "FluxoLiberacaoLCAdicionalID" });
            DropIndex("dbo.PropostaLCAdicionalComite", new[] { "PropostaLCAdicionalComiteSolicitanteID" });
            DropIndex("dbo.PropostaLCAdicionalComite", new[] { "UsuarioID" });
            DropIndex("dbo.PropostaLCAdicionalComite", new[] { "PerfilID" });
            DropIndex("dbo.PropostaLCAdicionalComite", new[] { "PropostaLCAdicionalID" });
            DropTable("dbo.PropostaLCAdicionalComite");
        }
    }
}
