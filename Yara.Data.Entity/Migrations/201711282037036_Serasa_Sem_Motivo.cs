namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Serasa_Sem_Motivo : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.SolicitanteSerasa", "MotivoConsulta", c => c.String(maxLength: 100, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SolicitanteSerasa", "MotivoConsulta", c => c.String(nullable: false, maxLength: 100, unicode: false));
        }
    }
}
