namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FluxoLiberacaoLCAdicional_Estrutura : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FluxoLiberacaoLCAdicional",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Nivel = c.Int(nullable: false),
                        ValorDe = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ValorAte = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Ativo = c.Boolean(nullable: false),
                        EmpresaID = c.String(maxLength: 120, unicode: false),
                        PrimeiroPerfilID = c.Guid(nullable: false),
                        SegundoPerfilID = c.Guid(),
                        UsuarioIDCriacao = c.Guid(nullable: false),
                        UsuarioIDAlteracao = c.Guid(),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Perfil", t => t.PrimeiroPerfilID)
                .ForeignKey("dbo.Perfil", t => t.SegundoPerfilID)
                .Index(t => t.PrimeiroPerfilID)
                .Index(t => t.SegundoPerfilID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FluxoLiberacaoLCAdicional", "SegundoPerfilID", "dbo.Perfil");
            DropForeignKey("dbo.FluxoLiberacaoLCAdicional", "PrimeiroPerfilID", "dbo.Perfil");
            DropIndex("dbo.FluxoLiberacaoLCAdicional", new[] { "SegundoPerfilID" });
            DropIndex("dbo.FluxoLiberacaoLCAdicional", new[] { "PrimeiroPerfilID" });
            DropTable("dbo.FluxoLiberacaoLCAdicional");
        }
    }
}
