namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ExplodeGrupo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GrupoEconomicoMembro", "ExplodeGrupo", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.GrupoEconomicoMembro", "ExplodeGrupo");
        }
    }
}
