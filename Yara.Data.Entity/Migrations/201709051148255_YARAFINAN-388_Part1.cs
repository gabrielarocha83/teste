namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class YARAFINAN388_Part1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.DivisaoRemessa", "QtdConfirmada");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DivisaoRemessa", "QtdConfirmada", c => c.Decimal(nullable: false, precision: 15, scale: 3));
        }
    }
}
