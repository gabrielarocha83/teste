namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrdemVendaFluxoEstruturadetabelasV1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrdemVendaFluxo", "Divisao", c => c.Int(nullable: false));
            AddColumn("dbo.OrdemVendaFluxo", "ItemOrdemVenda", c => c.Int(nullable: false));
            AddColumn("dbo.OrdemVendaFluxo", "OrdemVendaNumero", c => c.String(nullable: false, maxLength: 120, unicode: false));
            AlterColumn("dbo.OrdemVendaFluxo", "Status", c => c.String(nullable: false, maxLength: 2, unicode: false));
            DropColumn("dbo.OrdemVendaFluxo", "DivisaoRemessaDivisao");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OrdemVendaFluxo", "DivisaoRemessaDivisao", c => c.Int(nullable: false));
            AlterColumn("dbo.OrdemVendaFluxo", "Status", c => c.String(maxLength: 2, unicode: false));
            DropColumn("dbo.OrdemVendaFluxo", "OrdemVendaNumero");
            DropColumn("dbo.OrdemVendaFluxo", "ItemOrdemVenda");
            DropColumn("dbo.OrdemVendaFluxo", "Divisao");
        }
    }
}
