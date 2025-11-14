namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrdemdeVendaAlteracaodeestruturav9 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.DivisaoRemessa", new[] { "ItemOrdemVenda_Item", "ItemOrdemVenda_OrdemVendaNumero" }, "dbo.ItemOrdemVenda");
            DropIndex("dbo.DivisaoRemessa", new[] { "ItemOrdemVenda_Item", "ItemOrdemVenda_OrdemVendaNumero" });
            DropColumn("dbo.DivisaoRemessa", "ItemOrdemVenda_Item");
            DropColumn("dbo.DivisaoRemessa", "ItemOrdemVenda_OrdemVendaNumero");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DivisaoRemessa", "ItemOrdemVenda_OrdemVendaNumero", c => c.String(maxLength: 10, unicode: false));
            AddColumn("dbo.DivisaoRemessa", "ItemOrdemVenda_Item", c => c.Int());
            CreateIndex("dbo.DivisaoRemessa", new[] { "ItemOrdemVenda_Item", "ItemOrdemVenda_OrdemVendaNumero" });
            AddForeignKey("dbo.DivisaoRemessa", new[] { "ItemOrdemVenda_Item", "ItemOrdemVenda_OrdemVendaNumero" }, "dbo.ItemOrdemVenda", new[] { "Item", "OrdemVendaNumero" });
        }
    }
}
