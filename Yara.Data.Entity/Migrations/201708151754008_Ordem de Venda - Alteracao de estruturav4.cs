namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrdemdeVendaAlteracaodeestruturav4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DivisaoRemessa", "BloqueioPortal", c => c.String(maxLength: 2, unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DivisaoRemessa", "BloqueioPortal");
        }
    }
}
