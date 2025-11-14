namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrdemVendaAddCamponaEstrutura : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrdemVenda", "BloqueioFaturamento", c => c.String(nullable: false, maxLength: 2, unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrdemVenda", "BloqueioFaturamento");
            DropTable("dbo.StatusOrdemVendas");
        }
    }
}
