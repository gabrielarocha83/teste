namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Cultura_Nova : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cultura", "Descricao", c => c.String(nullable: false, maxLength: 50, unicode: false));
            AlterColumn("dbo.Cultura", "UnidadeMedida", c => c.String(nullable: false, maxLength: 10, unicode: false));
            DropColumn("dbo.Cultura", "Tipo");
            DropColumn("dbo.Cultura", "Sigla");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Cultura", "Sigla", c => c.String(maxLength: 120, unicode: false));
            AddColumn("dbo.Cultura", "Tipo", c => c.String(nullable: false, maxLength: 50, unicode: false));
            AlterColumn("dbo.Cultura", "UnidadeMedida", c => c.String(maxLength: 120, unicode: false));
            DropColumn("dbo.Cultura", "Descricao");
        }
    }
}
