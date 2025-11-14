namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addcampodenumerointernofluxoordemdevenda : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SolicitanteFluxo", "NumeroInternoProposta", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SolicitanteFluxo", "NumeroInternoProposta");
        }
    }
}
