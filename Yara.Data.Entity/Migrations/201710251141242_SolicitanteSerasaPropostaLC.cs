namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SolicitanteSerasaPropostaLC : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PropostaLC", "SolicitanteSerasaID", c => c.Guid());
            CreateIndex("dbo.PropostaLC", "SolicitanteSerasaID");
            AddForeignKey("dbo.PropostaLC", "SolicitanteSerasaID", "dbo.SolicitanteSerasa", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PropostaLC", "SolicitanteSerasaID", "dbo.SolicitanteSerasa");
            DropIndex("dbo.PropostaLC", new[] { "SolicitanteSerasaID" });
            DropColumn("dbo.PropostaLC", "SolicitanteSerasaID");
        }
    }
}
