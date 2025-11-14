namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CampoSinistroeDebitoAbono : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PropostaAbono", "TotalDebito", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.PropostaAbono", "Sinistro", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PropostaAbono", "Sinistro");
            DropColumn("dbo.PropostaAbono", "TotalDebito");
        }
    }
}
