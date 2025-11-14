namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProcessamentoCarteiraAddNovosCampos : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProcessamentoCarteira", "OrdemVenda", c => c.String(maxLength: 10, fixedLength: true, unicode: false));
            AddColumn("dbo.ProcessamentoCarteira", "SolicitanteFluxoID", c => c.Guid());
            CreateIndex("dbo.ProcessamentoCarteira", "SolicitanteFluxoID");
            AddForeignKey("dbo.ProcessamentoCarteira", "SolicitanteFluxoID", "dbo.SolicitanteFluxo", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProcessamentoCarteira", "SolicitanteFluxoID", "dbo.SolicitanteFluxo");
            DropIndex("dbo.ProcessamentoCarteira", new[] { "SolicitanteFluxoID" });
            DropColumn("dbo.ProcessamentoCarteira", "SolicitanteFluxoID");
            DropColumn("dbo.ProcessamentoCarteira", "OrdemVenda");
        }
    }
}
