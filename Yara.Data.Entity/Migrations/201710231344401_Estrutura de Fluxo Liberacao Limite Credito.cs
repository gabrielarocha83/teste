namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EstruturadeFluxoLiberacaoLimiteCredito : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FluxoLiberacaoLimiteCredito",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        OrdemLiberacao = c.Int(nullable: false),
                        Nivel = c.Int(nullable: false),
                        ValorDe = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ValorAte = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PerfilID = c.Guid(nullable: false),
                        UsuarioIDCriacao = c.Guid(nullable: false),
                        UsuarioIDAlteracao = c.Guid(),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Perfil", t => t.PerfilID)
                .Index(t => t.PerfilID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FluxoLiberacaoLimiteCredito", "PerfilID", "dbo.Perfil");
            DropIndex("dbo.FluxoLiberacaoLimiteCredito", new[] { "PerfilID" });
            DropTable("dbo.FluxoLiberacaoLimiteCredito");
        }
    }
}
