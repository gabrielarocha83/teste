namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ContClient : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ContaCliente", "TOP10", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ContaCliente", "TOP10");
        }
    }
}
