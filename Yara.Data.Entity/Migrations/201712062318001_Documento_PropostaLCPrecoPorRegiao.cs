namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Documento_PropostaLCPrecoPorRegiao : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PropostaLCPrecoPorRegiao", "Documento", c => c.String(maxLength: 120, unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PropostaLCPrecoPorRegiao", "Documento");
        }
    }
}
