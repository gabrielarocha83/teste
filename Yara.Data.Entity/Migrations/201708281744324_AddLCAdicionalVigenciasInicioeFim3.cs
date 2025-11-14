namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLCAdicionalVigenciasInicioeFim3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ContaClienteFinanceiro", "LC", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.ContaClienteFinanceiro", "LCDisponivel", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.ContaClienteFinanceiro", "LCAdicional", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.ContaClienteFinanceiro", "Exposicao", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.ContaClienteFinanceiro", "Recebiveis", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.ContaClienteFinanceiro", "OperacaoFinanciamento", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ContaClienteFinanceiro", "OperacaoFinanciamento", c => c.Single(nullable: false));
            AlterColumn("dbo.ContaClienteFinanceiro", "Recebiveis", c => c.Single(nullable: false));
            AlterColumn("dbo.ContaClienteFinanceiro", "Exposicao", c => c.Single());
            AlterColumn("dbo.ContaClienteFinanceiro", "LCAdicional", c => c.Single());
            AlterColumn("dbo.ContaClienteFinanceiro", "LCDisponivel", c => c.Single());
            AlterColumn("dbo.ContaClienteFinanceiro", "LC", c => c.Single());
        }
    }
}
