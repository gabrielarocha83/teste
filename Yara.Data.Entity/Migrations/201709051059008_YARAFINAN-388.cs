namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class YARAFINAN388 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DivisaoRemessa", "QtdEntregue", c => c.Decimal(nullable: false, precision: 15, scale: 3));
            AddColumn("dbo.DivisaoRemessa", "DataRemessa", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.DivisaoRemessa", "DataRemessa");
            DropColumn("dbo.DivisaoRemessa", "QtdEntregue");
        }
    }
}
