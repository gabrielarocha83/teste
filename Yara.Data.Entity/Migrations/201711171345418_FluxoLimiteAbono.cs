namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FluxoLimiteAbono : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FluxoLiberacaoAbono",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        SegmentoID = c.Guid(nullable: false),
                        Nivel = c.Int(nullable: false),
                        ValorDe = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ValorAte = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PerfilID = c.Guid(nullable: false),
                        Ativo = c.Boolean(nullable: false),
                        EmpresaID = c.String(maxLength: 120, unicode: false),
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
            DropForeignKey("dbo.FluxoLiberacaoAbono", "SegmentoID", "dbo.Segmento");
            DropForeignKey("dbo.FluxoLiberacaoAbono", "PerfilID", "dbo.Perfil");
            DropIndex("dbo.FluxoLiberacaoAbono", new[] { "PerfilID" });
            DropIndex("dbo.FluxoLiberacaoAbono", new[] { "SegmentoID" });
            DropTable("dbo.FluxoLiberacaoAbono");
        }
    }
}
