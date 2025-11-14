namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CodigoGrupoEconomicoAutoIncrementUnique : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GrupoEconomico", "Codigo", c => c.Int(nullable: false));
            CreateIndex("dbo.GrupoEconomico", "Codigo", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.GrupoEconomico", new[] { "Codigo" });
            DropColumn("dbo.GrupoEconomico", "Codigo");
        }
    }
}
