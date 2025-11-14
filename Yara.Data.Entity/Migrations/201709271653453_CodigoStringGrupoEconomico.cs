namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CodigoStringGrupoEconomico : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GrupoEconomico", "CodigoGrupo", c => c.String(nullable: false, maxLength: 10, unicode: false));
            DropColumn("dbo.GrupoEconomico", "Codigo");
        }
        
        public override void Down()
        {
            AddColumn("dbo.GrupoEconomico", "Codigo", c => c.Int(nullable: false, identity: true));
            DropColumn("dbo.GrupoEconomico", "CodigoGrupo");
        }
    }
}
