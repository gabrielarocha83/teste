namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Campo_Ativo_StatusCobranca : DbMigration
    {
        public override void Up()
        {
            //AddColumn("dbo.GrupoEconomicoMembro", "LCAntesGrupo", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.StatusCobranca", "Ativo", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.StatusCobranca", "Ativo");
            //DropColumn("dbo.GrupoEconomicoMembro", "LCAntesGrupo");
        }
    }
}
