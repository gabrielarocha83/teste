namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrdemVendaEstruturas : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LiberacaoOrdemVendaFluxo", "CodigoSap", c => c.String(maxLength: 10, unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.LiberacaoOrdemVendaFluxo", "CodigoSap");
        }
    }
}
