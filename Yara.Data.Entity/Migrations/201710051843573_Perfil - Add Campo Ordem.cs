namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PerfilAddCampoOrdem : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Perfil", "Ordem", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Perfil", "Ordem");
        }
    }
}
