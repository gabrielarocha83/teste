namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveIndexAndGrupoIdentity : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.GrupoEconomico", new[] { "Codigo" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.GrupoEconomico", "Codigo", unique: true);
        }
    }
}
