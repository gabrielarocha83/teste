namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Campo_Custo_CulturaEstado : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CulturaEstado", "Custo", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CulturaEstado", "Custo");
        }
    }
}
