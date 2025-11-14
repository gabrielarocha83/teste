namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Campo_MotivoConsulta_SolicitanteSerasa : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SolicitanteSerasa", "MotivoConsulta", c => c.String(nullable: false, maxLength: 100, unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SolicitanteSerasa", "MotivoConsulta");
        }
    }
}
