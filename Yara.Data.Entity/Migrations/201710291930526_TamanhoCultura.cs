namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TamanhoCultura : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Cultura", "UnidadeMedida", c => c.String(nullable: false, maxLength: 30, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Cultura", "UnidadeMedida", c => c.String(nullable: false, maxLength: 10, unicode: false));
        }
    }
}
