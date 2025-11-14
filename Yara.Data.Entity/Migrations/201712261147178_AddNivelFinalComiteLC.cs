namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNivelFinalComiteLC : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PropostaLCComite", "NivelFinal", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PropostaLCComite", "NivelFinal");
        }
    }
}
