namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveLCDisponivelContaCLienteFinanceiro : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ContaClienteFinanceiro", "LCDisponivel");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ContaClienteFinanceiro", "LCDisponivel", c => c.Decimal(precision: 18, scale: 2));
        }
    }
}
