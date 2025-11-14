namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCampoDocumentoPropostaLC : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PropostaLC", "Documento", c => c.String(maxLength: 280, unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PropostaLC", "Documento");
        }
    }
}
