namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RelacionamentoOrdemVendaFluxo : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.OrdemVendaFluxo", "SolicitanteFluxoID");
            AddForeignKey("dbo.OrdemVendaFluxo", "SolicitanteFluxoID", "dbo.SolicitanteFluxo", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrdemVendaFluxo", "SolicitanteFluxoID", "dbo.SolicitanteFluxo");
            DropIndex("dbo.OrdemVendaFluxo", new[] { "SolicitanteFluxoID" });
        }
    }
}
