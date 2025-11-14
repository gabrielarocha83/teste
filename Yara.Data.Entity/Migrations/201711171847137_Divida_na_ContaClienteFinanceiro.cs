namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Divida_na_ContaClienteFinanceiro : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ContaClienteFinanceiro", "DividaAtiva", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ContaClienteFinanceiro", "DividaAtiva");
        }
    }
}
