namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEmpresaFluxo02 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FluxoLiberacaoOrdemVenda", "EmpresaID", c => c.String(maxLength: 120, unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.FluxoLiberacaoOrdemVenda", "EmpresaID");
        }
    }
}
