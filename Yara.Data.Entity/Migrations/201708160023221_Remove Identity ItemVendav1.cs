namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveIdentityItemVendav1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.DivisaoRemessa", new[] { "ItemOrdemVendaItem", "OrdemVendaNumero" }, "dbo.ItemOrdemVenda");
            DropIndex("dbo.DivisaoRemessa", new[] { "ItemOrdemVendaItem", "OrdemVendaNumero" });
            DropIndex("dbo.ItemOrdemVenda", new[] { "OrdemVendaNumero" });
            RenameColumn(table: "dbo.ItemOrdemVenda", name: "OrdemVendaNumero", newName: "OrdemVenda_Numero");
            DropPrimaryKey("dbo.ItemOrdemVenda");
            AlterColumn("dbo.ItemOrdemVenda", "OrdemVenda_Numero", c => c.String(maxLength: 10, unicode: false));
            AddPrimaryKey("dbo.ItemOrdemVenda", "Item");
            CreateIndex("dbo.DivisaoRemessa", "ItemOrdemVendaItem");
            CreateIndex("dbo.ItemOrdemVenda", "OrdemVenda_Numero");
            AddForeignKey("dbo.DivisaoRemessa", "ItemOrdemVendaItem", "dbo.ItemOrdemVenda", "Item");
            DropColumn("dbo.ItemOrdemVenda", "Material");
            DropColumn("dbo.ItemOrdemVenda", "Centro");
            DropColumn("dbo.ItemOrdemVenda", "Deposito");
            DropColumn("dbo.ItemOrdemVenda", "CondPagto");
            DropColumn("dbo.ItemOrdemVenda", "CondFrete");
            DropColumn("dbo.ItemOrdemVenda", "Moeda");
            DropColumn("dbo.ItemOrdemVenda", "TaxaCambio");
            DropColumn("dbo.ItemOrdemVenda", "DataEfetivaFixa");
            DropColumn("dbo.ItemOrdemVenda", "MotivoRecusa");
            DropColumn("dbo.ItemOrdemVenda", "QtdPedida");
            DropColumn("dbo.ItemOrdemVenda", "QtdEntregue");
            DropColumn("dbo.ItemOrdemVenda", "UnidadeMedida");
            DropColumn("dbo.ItemOrdemVenda", "ValorUnitario");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ItemOrdemVenda", "ValorUnitario", c => c.Decimal(nullable: false, precision: 13, scale: 2));
            AddColumn("dbo.ItemOrdemVenda", "UnidadeMedida", c => c.String(maxLength: 3, unicode: false));
            AddColumn("dbo.ItemOrdemVenda", "QtdEntregue", c => c.Decimal(nullable: false, precision: 15, scale: 3));
            AddColumn("dbo.ItemOrdemVenda", "QtdPedida", c => c.Decimal(nullable: false, precision: 15, scale: 3));
            AddColumn("dbo.ItemOrdemVenda", "MotivoRecusa", c => c.String(maxLength: 2, unicode: false));
            AddColumn("dbo.ItemOrdemVenda", "DataEfetivaFixa", c => c.DateTime());
            AddColumn("dbo.ItemOrdemVenda", "TaxaCambio", c => c.Decimal(precision: 9, scale: 5));
            AddColumn("dbo.ItemOrdemVenda", "Moeda", c => c.String(maxLength: 4, unicode: false));
            AddColumn("dbo.ItemOrdemVenda", "CondFrete", c => c.String(maxLength: 3, fixedLength: true, unicode: false));
            AddColumn("dbo.ItemOrdemVenda", "CondPagto", c => c.String(maxLength: 4, fixedLength: true, unicode: false));
            AddColumn("dbo.ItemOrdemVenda", "Deposito", c => c.String(maxLength: 4, unicode: false));
            AddColumn("dbo.ItemOrdemVenda", "Centro", c => c.String(maxLength: 4, unicode: false));
            AddColumn("dbo.ItemOrdemVenda", "Material", c => c.String(nullable: false, maxLength: 18, unicode: false));
            DropForeignKey("dbo.DivisaoRemessa", "ItemOrdemVendaItem", "dbo.ItemOrdemVenda");
            DropIndex("dbo.ItemOrdemVenda", new[] { "OrdemVenda_Numero" });
            DropIndex("dbo.DivisaoRemessa", new[] { "ItemOrdemVendaItem" });
            DropPrimaryKey("dbo.ItemOrdemVenda");
            AlterColumn("dbo.ItemOrdemVenda", "OrdemVenda_Numero", c => c.String(nullable: false, maxLength: 10, unicode: false));
            AddPrimaryKey("dbo.ItemOrdemVenda", new[] { "Item", "OrdemVendaNumero" });
            RenameColumn(table: "dbo.ItemOrdemVenda", name: "OrdemVenda_Numero", newName: "OrdemVendaNumero");
            CreateIndex("dbo.ItemOrdemVenda", "OrdemVendaNumero");
            CreateIndex("dbo.DivisaoRemessa", new[] { "ItemOrdemVendaItem", "OrdemVendaNumero" });
            AddForeignKey("dbo.DivisaoRemessa", new[] { "ItemOrdemVendaItem", "OrdemVendaNumero" }, "dbo.ItemOrdemVenda", new[] { "Item", "OrdemVendaNumero" });
        }
    }
}
