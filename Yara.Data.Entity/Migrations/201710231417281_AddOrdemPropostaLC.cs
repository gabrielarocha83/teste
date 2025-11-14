namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOrdemPropostaLC : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PropostaLCStatus", "Ordem", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PropostaLCStatus", "Ordem");
        }
    }
}
