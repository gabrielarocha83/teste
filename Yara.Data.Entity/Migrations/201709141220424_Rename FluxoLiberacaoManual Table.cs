namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameFluxoLiberacaoManualTable : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.FluxoLimiteCredito", newName: "FluxoLiberacaoManual");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.FluxoLiberacaoManual", newName: "FluxoLimiteCredito");
        }
    }
}
