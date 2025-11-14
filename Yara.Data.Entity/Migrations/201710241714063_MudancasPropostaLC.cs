namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MudancasPropostaLC : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.PropostaLC", new[] { "ResponsavelID" });
            AddColumn("dbo.PropostaLC", "CodigoSap", c => c.String(maxLength: 120, unicode: false));
            AlterColumn("dbo.PropostaLC", "ResponsavelID", c => c.Guid());
            CreateIndex("dbo.PropostaLC", "ResponsavelID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.PropostaLC", new[] { "ResponsavelID" });
            AlterColumn("dbo.PropostaLC", "ResponsavelID", c => c.Guid(nullable: false));
            DropColumn("dbo.PropostaLC", "CodigoSap");
            CreateIndex("dbo.PropostaLC", "ResponsavelID");
        }
    }
}
