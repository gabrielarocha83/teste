namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DocumentoSerasa : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SolicitanteSerasa", "Documento", c => c.String(maxLength: 120, unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SolicitanteSerasa", "Documento");
        }
    }
}
