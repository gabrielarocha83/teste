namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CulturacomUnidadedeMedida : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cultura", "UnidadeMedida", c => c.String(maxLength: 120, unicode: false));
            AddColumn("dbo.Cultura", "Sigla", c => c.String(maxLength: 120, unicode: false));
            DropTable("dbo.UnidadeMedidaCultura");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.UnidadeMedidaCultura",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Nome = c.String(nullable: false, maxLength: 120, unicode: false),
                        Sigla = c.String(maxLength: 10, unicode: false),
                        Ativo = c.Boolean(nullable: false),
                        UsuarioIDCriacao = c.Guid(nullable: false),
                        UsuarioIDAlteracao = c.Guid(),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            DropColumn("dbo.Cultura", "Sigla");
            DropColumn("dbo.Cultura", "UnidadeMedida");
        }
    }
}
