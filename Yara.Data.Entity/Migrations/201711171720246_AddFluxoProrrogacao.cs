namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFluxoProrrogacao : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FluxoLiberacaoProrrogacao",
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
            DropForeignKey("dbo.FluxoLiberacaoProrrogacao", "SegmentoID", "dbo.Segmento");
            DropForeignKey("dbo.FluxoLiberacaoProrrogacao", "PerfilID", "dbo.Perfil");
            DropIndex("dbo.FluxoLiberacaoProrrogacao", new[] { "PerfilID" });
            DropIndex("dbo.FluxoLiberacaoProrrogacao", new[] { "SegmentoID" });
            DropTable("dbo.FluxoLiberacaoProrrogacao");
        }
    }
}
