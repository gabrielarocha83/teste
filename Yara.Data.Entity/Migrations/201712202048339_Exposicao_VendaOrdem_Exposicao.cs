namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Exposicao_VendaOrdem_Exposicao : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ContaClienteFinanceiro", "ExposicaoVendaOrdem", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.ContaClienteFinanceiro", "ExposicaoExportacao", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.ItemOrdemVenda", "QtdVendaOrdem", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.ItemOrdemVenda", "QtdExportacao", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ItemOrdemVenda", "QtdExportacao");
            DropColumn("dbo.ItemOrdemVenda", "QtdVendaOrdem");
            DropColumn("dbo.ContaClienteFinanceiro", "ExposicaoExportacao");
            DropColumn("dbo.ContaClienteFinanceiro", "ExposicaoVendaOrdem");
        }
    }
}
