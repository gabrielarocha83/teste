namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddcamposnatabelaStatusdeCobrançav2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StatusCobranca", "NaoCobranca", c => c.Boolean(nullable: false));
            AddColumn("dbo.StatusCobranca", "ContaExposicao", c => c.Boolean(nullable: false));
            AddColumn("dbo.StatusCobranca", "BloqueioOrdem", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.StatusCobranca", "BloqueioOrdem");
            DropColumn("dbo.StatusCobranca", "ContaExposicao");
            DropColumn("dbo.StatusCobranca", "NaoCobranca");
        }
    }
}
