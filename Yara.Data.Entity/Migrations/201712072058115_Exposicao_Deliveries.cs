namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Exposicao_Deliveries : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ContaClienteFinanceiro", "ExposicaoRemessas", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ContaClienteFinanceiro", "ExposicaoRemessas");
        }
    }
}
