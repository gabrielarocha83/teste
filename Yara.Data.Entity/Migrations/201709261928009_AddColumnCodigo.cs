namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumnCodigo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GrupoEconomico", "Codigo", c => c.Int(nullable: false, identity: true));
        }
        
        public override void Down()
        {
            DropColumn("dbo.GrupoEconomico", "Codigo");
        }
    }
}
