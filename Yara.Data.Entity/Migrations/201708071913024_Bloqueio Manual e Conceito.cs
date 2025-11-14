namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BloqueioManualeConceito : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ContaCliente", "BloqueioManual", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ContaCliente", "BloqueioManual");
        }
    }
}
