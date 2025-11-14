namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TipoClassificacaoGrupoEconomico : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GrupoEconomico", "ClassificacaoGrupoEconomicoID", c => c.Int(nullable: false));
            CreateIndex("dbo.GrupoEconomico", "ClassificacaoGrupoEconomicoID");
            AddForeignKey("dbo.GrupoEconomico", "ClassificacaoGrupoEconomicoID", "dbo.ClassificacaoGrupoEconomico", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GrupoEconomico", "ClassificacaoGrupoEconomicoID", "dbo.ClassificacaoGrupoEconomico");
            DropIndex("dbo.GrupoEconomico", new[] { "ClassificacaoGrupoEconomicoID" });
            DropColumn("dbo.GrupoEconomico", "ClassificacaoGrupoEconomicoID");
        }
    }
}
