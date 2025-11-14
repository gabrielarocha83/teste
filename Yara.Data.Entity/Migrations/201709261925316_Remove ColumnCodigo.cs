namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveColumnCodigo : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.GrupoEconomico", "Codigo");
        }
        
        public override void Down()
        {
            AddColumn("dbo.GrupoEconomico", "Codigo", c => c.Int(nullable: false, identity: true));
        }
    }
}
