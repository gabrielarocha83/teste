namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrdemdeVendaAlteracaodeestruturav5 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.DivisaoRemessa", "BloqueioPortal", c => c.String(maxLength: 120, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.DivisaoRemessa", "BloqueioPortal", c => c.String(maxLength: 2, unicode: false));
        }
    }
}
