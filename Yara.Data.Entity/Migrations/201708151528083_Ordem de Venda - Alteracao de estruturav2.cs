namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrdemdeVendaAlteracaodeestruturav2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.DivisaoRemessa", "DataOrganizacao", c => c.DateTime());
            AlterColumn("dbo.DivisaoRemessa", "DataPreparacao", c => c.DateTime());
            AlterColumn("dbo.DivisaoRemessa", "DataCarregamento", c => c.DateTime());
            AlterColumn("dbo.DivisaoRemessa", "Status", c => c.String(maxLength: 2, unicode: false));
            AlterColumn("dbo.ItemOrdemVenda", "TaxaCambio", c => c.Decimal(precision: 9, scale: 5));
            AlterColumn("dbo.ItemOrdemVenda", "DataEfetivaFixa", c => c.DateTime());
            AlterColumn("dbo.OrdemVenda", "PedidoCliente", c => c.String(maxLength: 20, unicode: false));
            AlterColumn("dbo.OrdemVenda", "TaxaCambio", c => c.Decimal(precision: 9, scale: 5));
            AlterColumn("dbo.OrdemVenda", "DataEfetivaFixa", c => c.DateTime());
            AlterColumn("dbo.OrdemVenda", "DataModificacao", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.OrdemVenda", "DataModificacao", c => c.DateTime(nullable: false));
            AlterColumn("dbo.OrdemVenda", "DataEfetivaFixa", c => c.DateTime(nullable: false));
            AlterColumn("dbo.OrdemVenda", "TaxaCambio", c => c.Decimal(nullable: false, precision: 9, scale: 5));
            AlterColumn("dbo.OrdemVenda", "PedidoCliente", c => c.String(nullable: false, maxLength: 20, unicode: false));
            AlterColumn("dbo.ItemOrdemVenda", "DataEfetivaFixa", c => c.DateTime(nullable: false));
            AlterColumn("dbo.ItemOrdemVenda", "TaxaCambio", c => c.Decimal(nullable: false, precision: 9, scale: 5));
            AlterColumn("dbo.DivisaoRemessa", "Status", c => c.String(nullable: false, maxLength: 2, unicode: false));
            AlterColumn("dbo.DivisaoRemessa", "DataCarregamento", c => c.DateTime(nullable: false));
            AlterColumn("dbo.DivisaoRemessa", "DataPreparacao", c => c.DateTime(nullable: false));
            AlterColumn("dbo.DivisaoRemessa", "DataOrganizacao", c => c.DateTime(nullable: false));
        }
    }
}
