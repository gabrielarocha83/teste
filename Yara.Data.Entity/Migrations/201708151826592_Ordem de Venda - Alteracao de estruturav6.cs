namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrdemdeVendaAlteracaodeestruturav6 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.DivisaoRemessa", "ItemOrdemVendaItem", "dbo.ItemOrdemVenda");
            DropIndex("dbo.DivisaoRemessa", new[] { "ItemOrdemVendaItem" });
            DropPrimaryKey("dbo.ItemOrdemVenda");
            AddColumn("dbo.DivisaoRemessa", "ItemOrdemVenda_Item", c => c.Int());
            AddColumn("dbo.DivisaoRemessa", "ItemOrdemVenda_OrdemVendaNumero", c => c.String(maxLength: 10, unicode: false));
            AlterColumn("dbo.ItemOrdemVenda", "Item", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.ItemOrdemVenda", new[] { "Item", "OrdemVendaNumero" });
            CreateIndex("dbo.DivisaoRemessa", new[] { "ItemOrdemVenda_Item", "ItemOrdemVenda_OrdemVendaNumero" });
            AddForeignKey("dbo.DivisaoRemessa", new[] { "ItemOrdemVenda_Item", "ItemOrdemVenda_OrdemVendaNumero" }, "dbo.ItemOrdemVenda", new[] { "Item", "OrdemVendaNumero" });
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DivisaoRemessa", new[] { "ItemOrdemVenda_Item", "ItemOrdemVenda_OrdemVendaNumero" }, "dbo.ItemOrdemVenda");
            DropIndex("dbo.DivisaoRemessa", new[] { "ItemOrdemVenda_Item", "ItemOrdemVenda_OrdemVendaNumero" });
            DropPrimaryKey("dbo.ItemOrdemVenda");
            AlterColumn("dbo.ItemOrdemVenda", "Item", c => c.Int(nullable: false, identity: true));
            DropColumn("dbo.DivisaoRemessa", "ItemOrdemVenda_OrdemVendaNumero");
            DropColumn("dbo.DivisaoRemessa", "ItemOrdemVenda_Item");
            AddPrimaryKey("dbo.ItemOrdemVenda", "Item");
            CreateIndex("dbo.DivisaoRemessa", "ItemOrdemVendaItem");
            AddForeignKey("dbo.DivisaoRemessa", "ItemOrdemVendaItem", "dbo.ItemOrdemVenda", "Item");
        }
    }
}
