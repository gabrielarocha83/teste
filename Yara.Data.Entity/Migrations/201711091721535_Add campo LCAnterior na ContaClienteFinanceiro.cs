namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddcampoLCAnteriornaContaClienteFinanceiro : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ContaClienteFinanceiro", "LCAnterior", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.ContaClienteFinanceiro", "VigenciaAnterior", c => c.DateTime());
            AddColumn("dbo.ContaClienteFinanceiro", "VigenciaFimAnterior", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ContaClienteFinanceiro", "VigenciaFimAnterior");
            DropColumn("dbo.ContaClienteFinanceiro", "VigenciaAnterior");
            DropColumn("dbo.ContaClienteFinanceiro", "LCAnterior");
        }
    }
}
