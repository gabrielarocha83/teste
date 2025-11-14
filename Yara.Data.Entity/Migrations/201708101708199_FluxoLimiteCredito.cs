namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FluxoLimiteCredito : DbMigration
    {
        public override void Up()
        {

            
            CreateTable(
                "dbo.FluxoLimiteCredito",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Nivel = c.Int(nullable: false),
                        ValorDe = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ValorAte = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Usuario = c.Guid(nullable: false),
                        Grupo = c.Guid(nullable: false),
                        Estrutura = c.Guid(nullable: false),
                        UsuarioIDCriacao = c.Guid(nullable: false),
                        UsuarioIDAlteracao = c.Guid(),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.FluxoLimiteCredito");
          
        }
    }
}
