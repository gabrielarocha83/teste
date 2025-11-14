namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EstruturasOrdemVendaNovas : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.OrdemVendaFluxo", "FluxoLiberacaoManualID", "dbo.FluxoLiberacaoManual");
            DropIndex("dbo.OrdemVendaFluxo", new[] { "FluxoLiberacaoManualID" });
            CreateTable(
                "dbo.LiberacaoOrdemVendaFluxo",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        SolicitanteFluxoID = c.Guid(nullable: false),
                        FluxoLiberacaoOrdemVendaID = c.Guid(nullable: false),
                        UsuarioID = c.Guid(),
                        StatusOrdemVendasID = c.Guid(nullable: false),
                        EmpresasId = c.String(maxLength: 1, fixedLength: true, unicode: false),
                        UsuarioIDCriacao = c.Guid(nullable: false),
                        UsuarioIDAlteracao = c.Guid(),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Empresa", t => t.EmpresasId)
                .ForeignKey("dbo.FluxoLiberacaoOrdemVenda", t => t.FluxoLiberacaoOrdemVendaID)
                .ForeignKey("dbo.SolicitanteFluxo", t => t.SolicitanteFluxoID)
                .ForeignKey("dbo.StatusOrdemVendas", t => t.StatusOrdemVendasID)
                .Index(t => t.SolicitanteFluxoID)
                .Index(t => t.FluxoLiberacaoOrdemVendaID)
                .Index(t => t.StatusOrdemVendasID)
                .Index(t => t.EmpresasId);
            
            AddColumn("dbo.SolicitanteFluxo", "Comentario", c => c.String(unicode: false, storeType: "text"));
            AddColumn("dbo.SolicitanteFluxo", "AcompanharProposta", c => c.Boolean(nullable: false));
            AddColumn("dbo.SolicitanteFluxo", "EmpresasId", c => c.String(maxLength: 1, fixedLength: true, unicode: false));
            AddColumn("dbo.SolicitanteFluxo", "Total", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.OrdemVendaFluxo", "EmpresasId", c => c.String(maxLength: 1, fixedLength: true, unicode: false));
            CreateIndex("dbo.SolicitanteFluxo", "EmpresasId");
            CreateIndex("dbo.OrdemVendaFluxo", "EmpresasId");
            AddForeignKey("dbo.SolicitanteFluxo", "EmpresasId", "dbo.Empresa", "ID");
            AddForeignKey("dbo.OrdemVendaFluxo", "EmpresasId", "dbo.Empresa", "ID");
            DropColumn("dbo.OrdemVendaFluxo", "FluxoLiberacaoManualID");
            DropColumn("dbo.OrdemVendaFluxo", "UsuarioID");
            DropColumn("dbo.OrdemVendaFluxo", "Status");
            DropColumn("dbo.OrdemVendaFluxo", "EmpresaId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OrdemVendaFluxo", "EmpresaId", c => c.String(maxLength: 1, fixedLength: true, unicode: false));
            AddColumn("dbo.OrdemVendaFluxo", "Status", c => c.String(nullable: false, maxLength: 2, unicode: false));
            AddColumn("dbo.OrdemVendaFluxo", "UsuarioID", c => c.Guid());
            AddColumn("dbo.OrdemVendaFluxo", "FluxoLiberacaoManualID", c => c.Guid(nullable: false));
            DropForeignKey("dbo.OrdemVendaFluxo", "EmpresasId", "dbo.Empresa");
            DropForeignKey("dbo.LiberacaoOrdemVendaFluxo", "StatusOrdemVendasID", "dbo.StatusOrdemVendas");
            DropForeignKey("dbo.LiberacaoOrdemVendaFluxo", "SolicitanteFluxoID", "dbo.SolicitanteFluxo");
            DropForeignKey("dbo.LiberacaoOrdemVendaFluxo", "FluxoLiberacaoOrdemVendaID", "dbo.FluxoLiberacaoOrdemVenda");
            DropForeignKey("dbo.LiberacaoOrdemVendaFluxo", "EmpresasId", "dbo.Empresa");
            DropForeignKey("dbo.SolicitanteFluxo", "EmpresasId", "dbo.Empresa");
            DropIndex("dbo.OrdemVendaFluxo", new[] { "EmpresasId" });
            DropIndex("dbo.LiberacaoOrdemVendaFluxo", new[] { "EmpresasId" });
            DropIndex("dbo.LiberacaoOrdemVendaFluxo", new[] { "StatusOrdemVendasID" });
            DropIndex("dbo.LiberacaoOrdemVendaFluxo", new[] { "FluxoLiberacaoOrdemVendaID" });
            DropIndex("dbo.LiberacaoOrdemVendaFluxo", new[] { "SolicitanteFluxoID" });
            DropIndex("dbo.SolicitanteFluxo", new[] { "EmpresasId" });
            DropColumn("dbo.OrdemVendaFluxo", "EmpresasId");
            DropColumn("dbo.SolicitanteFluxo", "Total");
            DropColumn("dbo.SolicitanteFluxo", "EmpresasId");
            DropColumn("dbo.SolicitanteFluxo", "AcompanharProposta");
            DropColumn("dbo.SolicitanteFluxo", "Comentario");
            DropTable("dbo.LiberacaoOrdemVendaFluxo");
            CreateIndex("dbo.OrdemVendaFluxo", "FluxoLiberacaoManualID");
            AddForeignKey("dbo.OrdemVendaFluxo", "FluxoLiberacaoManualID", "dbo.FluxoLiberacaoManual", "ID");
        }
    }
}
