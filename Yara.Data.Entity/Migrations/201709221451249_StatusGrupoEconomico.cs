namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StatusGrupoEconomico : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.GrupoEconomico", new[] { "StatusGrupoEconomicoFluxo_ID" });
            DropColumn("dbo.GrupoEconomico", "StatusGrupoEconomicoFluxoID");
            RenameColumn(table: "dbo.GrupoEconomico", name: "StatusGrupoEconomicoFluxo_ID", newName: "StatusGrupoEconomicoFluxoID");
            AlterColumn("dbo.GrupoEconomico", "StatusGrupoEconomicoFluxoID", c => c.String(nullable: false, maxLength: 3, unicode: false));
            AlterColumn("dbo.GrupoEconomico", "StatusGrupoEconomicoFluxoID", c => c.String(nullable: false, maxLength: 3, unicode: false));
            CreateIndex("dbo.GrupoEconomico", "StatusGrupoEconomicoFluxoID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.GrupoEconomico", new[] { "StatusGrupoEconomicoFluxoID" });
            AlterColumn("dbo.GrupoEconomico", "StatusGrupoEconomicoFluxoID", c => c.String(maxLength: 3, unicode: false));
            AlterColumn("dbo.GrupoEconomico", "StatusGrupoEconomicoFluxoID", c => c.Guid(nullable: false));
            RenameColumn(table: "dbo.GrupoEconomico", name: "StatusGrupoEconomicoFluxoID", newName: "StatusGrupoEconomicoFluxo_ID");
            AddColumn("dbo.GrupoEconomico", "StatusGrupoEconomicoFluxoID", c => c.Guid(nullable: false));
            CreateIndex("dbo.GrupoEconomico", "StatusGrupoEconomicoFluxo_ID");
        }
    }
}
