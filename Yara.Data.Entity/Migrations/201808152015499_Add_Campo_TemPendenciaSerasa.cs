namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Campo_TemPendenciaSerasa : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SolicitanteSerasa", "TemPendenciaSerasa", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SolicitanteSerasa", "TemPendenciaSerasa");
        }
    }
}
