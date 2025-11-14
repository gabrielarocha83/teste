namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Campos_Extras_PropostaLCCultura : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PropostaLCCultura", "MediaFertilizantePadrao", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.PropostaLCCultura", "PorcentagemFertilizanteCustoPadrao", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.PropostaLCCultura", "PrecoPadrao", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.PropostaLCCultura", "ProdutividadeMediaPadrao", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.PropostaLCCultura", "CustoPadrao", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PropostaLCCultura", "CustoPadrao");
            DropColumn("dbo.PropostaLCCultura", "ProdutividadeMediaPadrao");
            DropColumn("dbo.PropostaLCCultura", "PrecoPadrao");
            DropColumn("dbo.PropostaLCCultura", "PorcentagemFertilizanteCustoPadrao");
            DropColumn("dbo.PropostaLCCultura", "MediaFertilizantePadrao");
        }
    }
}
