namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FluxoAlcadaAnalise_FluxoAlcadaAprovacao_Estruturas : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FluxoAlcadaAnalise",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Nivel = c.Int(nullable: false),
                        ValorDe = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ValorAte = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Ativo = c.Boolean(nullable: false),
                        EmpresaID = c.String(maxLength: 120, unicode: false),
                        SegmentoID = c.Guid(nullable: false),
                        PerfilID = c.Guid(nullable: false),
                        UsuarioIDCriacao = c.Guid(nullable: false),
                        UsuarioIDAlteracao = c.Guid(),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Perfil", t => t.PerfilID)
                .ForeignKey("dbo.Segmento", t => t.SegmentoID)
                .Index(t => t.SegmentoID)
                .Index(t => t.PerfilID);
            
            CreateTable(
                "dbo.FluxoAlcadaAprovacao",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Nivel = c.Int(nullable: false),
                        ValorDe = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ValorAte = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Ativo = c.Boolean(nullable: false),
                        EmpresaID = c.String(maxLength: 120, unicode: false),
                        SegmentoID = c.Guid(nullable: false),
                        PerfilID = c.Guid(nullable: false),
                        UsuarioIDCriacao = c.Guid(nullable: false),
                        UsuarioIDAlteracao = c.Guid(),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Perfil", t => t.PerfilID)
                .ForeignKey("dbo.Segmento", t => t.SegmentoID)
                .Index(t => t.SegmentoID)
                .Index(t => t.PerfilID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FluxoAlcadaAprovacao", "SegmentoID", "dbo.Segmento");
            DropForeignKey("dbo.FluxoAlcadaAprovacao", "PerfilID", "dbo.Perfil");
            DropForeignKey("dbo.FluxoAlcadaAnalise", "SegmentoID", "dbo.Segmento");
            DropForeignKey("dbo.FluxoAlcadaAnalise", "PerfilID", "dbo.Perfil");
            DropIndex("dbo.FluxoAlcadaAprovacao", new[] { "PerfilID" });
            DropIndex("dbo.FluxoAlcadaAprovacao", new[] { "SegmentoID" });
            DropIndex("dbo.FluxoAlcadaAnalise", new[] { "PerfilID" });
            DropIndex("dbo.FluxoAlcadaAnalise", new[] { "SegmentoID" });
            DropTable("dbo.FluxoAlcadaAprovacao");
            DropTable("dbo.FluxoAlcadaAnalise");
        }
    }
}
