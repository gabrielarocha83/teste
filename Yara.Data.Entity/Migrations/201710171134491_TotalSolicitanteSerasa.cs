namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TotalSolicitanteSerasa : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SolicitanteSerasa", "Total", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SolicitanteSerasa", "Total");
        }
    }
}
