namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CodigoGrupoEconomicoIdentity : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.GrupoEconomico", new[] { "Codigo" });
            AlterColumn("dbo.GrupoEconomico", "Codigo", c => c.Int(nullable: false, identity: true));
            CreateIndex("dbo.GrupoEconomico", "Codigo", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.GrupoEconomico", new[] { "Codigo" });
            AlterColumn("dbo.GrupoEconomico", "Codigo", c => c.Int(nullable: false));
            CreateIndex("dbo.GrupoEconomico", "Codigo", unique: true);
        }
    }
}
