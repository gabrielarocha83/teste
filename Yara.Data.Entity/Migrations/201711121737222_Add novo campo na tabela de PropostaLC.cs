namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddnovocamponatabeladePropostaLC : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PropostaLC", "ParecerAnalista", c => c.String(maxLength: 400, unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PropostaLC", "ParecerAnalista");
        }
    }
}
