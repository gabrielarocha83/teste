namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PagaERetira_1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ItemOrdemVenda", "PagaRetira", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ItemOrdemVenda", "PagaRetira");
        }
    }
}
