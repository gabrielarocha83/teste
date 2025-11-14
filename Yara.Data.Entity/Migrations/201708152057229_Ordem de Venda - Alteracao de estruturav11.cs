namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrdemdeVendaAlteracaodeestruturav11 : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.DivisaoRemessa", new[] { "ItemOrdemVendaItem", "OrdemVendaNumero" });
            AddForeignKey("dbo.DivisaoRemessa", new[] { "ItemOrdemVendaItem", "OrdemVendaNumero" }, "dbo.ItemOrdemVenda", new[] { "Item", "OrdemVendaNumero" });
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DivisaoRemessa", new[] { "ItemOrdemVendaItem", "OrdemVendaNumero" }, "dbo.ItemOrdemVenda");
            DropIndex("dbo.DivisaoRemessa", new[] { "ItemOrdemVendaItem", "OrdemVendaNumero" });
        }
    }
}
