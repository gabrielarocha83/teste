namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLCAdicionalVigenciasInicioeFim4 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.ContaClienteFinanceiro", new[] { "ConceitoCobrancaID" });
            AlterColumn("dbo.ContaClienteFinanceiro", "ConceitoCobrancaID", c => c.Guid());
            AlterColumn("dbo.ContaClienteFinanceiro", "Recebiveis", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.ContaClienteFinanceiro", "OperacaoFinanciamento", c => c.Decimal(precision: 18, scale: 2));
            CreateIndex("dbo.ContaClienteFinanceiro", "ConceitoCobrancaID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.ContaClienteFinanceiro", new[] { "ConceitoCobrancaID" });
            AlterColumn("dbo.ContaClienteFinanceiro", "OperacaoFinanciamento", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.ContaClienteFinanceiro", "Recebiveis", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.ContaClienteFinanceiro", "ConceitoCobrancaID", c => c.Guid(nullable: false));
            CreateIndex("dbo.ContaClienteFinanceiro", "ConceitoCobrancaID");
        }
    }
}
