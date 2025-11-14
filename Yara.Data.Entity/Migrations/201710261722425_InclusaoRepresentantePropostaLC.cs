namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InclusaoRepresentantePropostaLC : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PropostaLC", "RepresentanteID", c => c.Guid());
            CreateIndex("dbo.PropostaLC", "RepresentanteID");
            AddForeignKey("dbo.PropostaLC", "RepresentanteID", "dbo.Representante", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PropostaLC", "RepresentanteID", "dbo.Representante");
            DropIndex("dbo.PropostaLC", new[] { "RepresentanteID" });
            DropColumn("dbo.PropostaLC", "RepresentanteID");
        }
    }
}
