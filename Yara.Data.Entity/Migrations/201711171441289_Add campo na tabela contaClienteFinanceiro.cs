namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddcamponatabelacontaClienteFinanceiro : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ContaClienteFinanceiro", "Pdd", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.ContaClienteFinanceiro", "Sinistro", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ContaClienteFinanceiro", "Sinistro");
            DropColumn("dbo.ContaClienteFinanceiro", "Pdd");
        }
    }
}
