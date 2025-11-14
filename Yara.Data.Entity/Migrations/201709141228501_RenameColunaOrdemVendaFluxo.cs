namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameColunaOrdemVendaFluxo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrdemVendaFluxo", "FluxoLiberacaoManualID", c => c.Guid(nullable: false));
            AlterColumn("dbo.OrdemVendaFluxo", "EmpresaId", c => c.String(maxLength: 1, fixedLength: true, unicode: false));
            CreateIndex("dbo.OrdemVendaFluxo", "FluxoLiberacaoManualID");
            AddForeignKey("dbo.OrdemVendaFluxo", "FluxoLiberacaoManualID", "dbo.FluxoLiberacaoManual", "ID");
            DropColumn("dbo.OrdemVendaFluxo", "FluxoLimiteCreditoID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OrdemVendaFluxo", "FluxoLimiteCreditoID", c => c.Guid(nullable: false));
            DropForeignKey("dbo.OrdemVendaFluxo", "FluxoLiberacaoManualID", "dbo.FluxoLiberacaoManual");
            DropIndex("dbo.OrdemVendaFluxo", new[] { "FluxoLiberacaoManualID" });
            AlterColumn("dbo.OrdemVendaFluxo", "EmpresaId", c => c.String(maxLength: 120, unicode: false));
            DropColumn("dbo.OrdemVendaFluxo", "FluxoLiberacaoManualID");
        }
    }
}
