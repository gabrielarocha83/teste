namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ContaClienteAddFlagRestricaoSerasa : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ContaCliente", "RestricaoSerasa", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ContaCliente", "RestricaoSerasa");
        }
    }
}
