namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StatusCobranca_New : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StatusCobranca",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Descricao = c.String(nullable: false, maxLength: 60, unicode: false),
                        CobrancaEfetiva = c.Boolean(nullable: false),
                        Padrao = c.Boolean(nullable: false),
                        UsuarioIDCriacao = c.Guid(nullable: false),
                        UsuarioIDAlteracao = c.Guid(),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.StatusCobranca");
        }
    }
}
