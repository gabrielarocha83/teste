namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddcamponatabelacontaClienteFinanceirodataseguradora : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ContaClienteFinanceiro", "DataSeguradora", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ContaClienteFinanceiro", "DataSeguradora");
        }
    }
}
