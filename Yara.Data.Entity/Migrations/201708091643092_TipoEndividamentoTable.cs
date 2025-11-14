namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TipoEndividamentoTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TipoEndividamento",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Tipo = c.String(nullable: false, maxLength: 120, unicode: false),
                        Ativo = c.Boolean(nullable: false),
                        UsuarioIDCriacao = c.Guid(nullable: false),
                        UsuarioIDAlteracao = c.Guid(),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TipoEndividamento");
        }
    }
}
