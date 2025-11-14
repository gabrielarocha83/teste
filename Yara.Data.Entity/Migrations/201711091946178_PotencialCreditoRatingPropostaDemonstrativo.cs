namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PotencialCreditoRatingPropostaDemonstrativo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PropostaLC", "Rating", c => c.String(maxLength: 120, unicode: false));
            AddColumn("dbo.PropostaLC", "PotencialCredito", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.PropostaLCDemonstrativo", "Rating", c => c.String(maxLength: 120, unicode: false));
            AddColumn("dbo.PropostaLCDemonstrativo", "PotencialCredito", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PropostaLCDemonstrativo", "PotencialCredito");
            DropColumn("dbo.PropostaLCDemonstrativo", "Rating");
            DropColumn("dbo.PropostaLC", "PotencialCredito");
            DropColumn("dbo.PropostaLC", "Rating");
        }
    }
}
