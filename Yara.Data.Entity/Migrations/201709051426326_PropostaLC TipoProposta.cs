namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PropostaLCTipoProposta : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PropostaLC", "TipoProposta", c => c.String(maxLength: 120, unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PropostaLC", "TipoProposta");
        }
    }
}
