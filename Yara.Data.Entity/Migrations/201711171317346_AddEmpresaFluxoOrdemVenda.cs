namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEmpresaFluxoOrdemVenda : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FluxoLiberacaoLimiteCredito", "EmpresaID", c => c.String(maxLength: 120, unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.FluxoLiberacaoLimiteCredito", "EmpresaID");
        }
    }
}
