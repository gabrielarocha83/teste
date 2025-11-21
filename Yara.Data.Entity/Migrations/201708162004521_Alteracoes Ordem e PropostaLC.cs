namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlteraçõesOrdemePropostaLC : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PropostaLC", "PropostaLCStatusID", c => c.Guid(nullable: false));
            CreateIndex("dbo.PropostaLC", "PropostaLCStatusID");
            AddForeignKey("dbo.PropostaLC", "PropostaLCStatusID", "dbo.PropostaLCStatus", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PropostaLC", "PropostaLCStatusID", "dbo.PropostaLCStatus");
            DropIndex("dbo.PropostaLC", new[] { "PropostaLCStatusID" });
            DropColumn("dbo.PropostaLC", "PropostaLCStatusID");
        }
    }
}
