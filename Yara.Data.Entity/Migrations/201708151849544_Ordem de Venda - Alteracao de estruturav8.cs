namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrdemdeVendaAlteracaodeestruturav8 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.DivisaoRemessa", "OrdemVendaNumero", "dbo.OrdemVenda");
            DropIndex("dbo.DivisaoRemessa", new[] { "OrdemVendaNumero" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.DivisaoRemessa", "OrdemVendaNumero");
            AddForeignKey("dbo.DivisaoRemessa", "OrdemVendaNumero", "dbo.OrdemVenda", "Numero");
        }
    }
}
