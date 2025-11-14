namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class QtdDelivery_Item : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ItemOrdemVenda", "QtdDelivery", c => c.Decimal(nullable: false, precision: 15, scale: 3));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ItemOrdemVenda", "QtdDelivery");
        }
    }
}
