namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrdemdeVendaAlteracaodeestruturav6 : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.DivisaoRemessa", "OrdemVendaNumero");
            AddForeignKey("dbo.DivisaoRemessa", "OrdemVendaNumero", "dbo.OrdemVenda", "Numero");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DivisaoRemessa", "OrdemVendaNumero", "dbo.OrdemVenda");
            DropIndex("dbo.DivisaoRemessa", new[] { "OrdemVendaNumero" });
        }
    }
}
