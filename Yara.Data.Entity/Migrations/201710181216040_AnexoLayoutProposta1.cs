namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AnexoLayoutProposta1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Anexo", "CategoriaDocumento", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Anexo", "CategoriaDocumento");
        }
    }
}
