namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SolicitanteDocumento : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PropostaAlcadaComercialDocumento", "SolicitanteSerasaID", c => c.Guid());
            CreateIndex("dbo.PropostaAlcadaComercialDocumento", "SolicitanteSerasaID");
            AddForeignKey("dbo.PropostaAlcadaComercialDocumento", "SolicitanteSerasaID", "dbo.SolicitanteSerasa", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PropostaAlcadaComercialDocumento", "SolicitanteSerasaID", "dbo.SolicitanteSerasa");
            DropIndex("dbo.PropostaAlcadaComercialDocumento", new[] { "SolicitanteSerasaID" });
            DropColumn("dbo.PropostaAlcadaComercialDocumento", "SolicitanteSerasaID");
        }
    }
}
