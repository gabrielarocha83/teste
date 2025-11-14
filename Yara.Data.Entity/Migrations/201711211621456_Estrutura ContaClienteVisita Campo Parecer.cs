namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EstruturaContaClienteVisitaCampoParecer : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ContaClienteVisita", "Parecer", c => c.String(unicode: false, storeType: "text"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ContaClienteVisita", "Parecer", c => c.String(nullable: false, unicode: false, storeType: "text"));
        }
    }
}
