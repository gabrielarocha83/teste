namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdicionadocamposnapropostaLC : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PropostaLCBemRural", "ValorTotal", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.PropostaLCBemUrbano", "ValorTotal", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.PropostaLCCultura", "PorcentagemFertilizanteCusto", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.PropostaLCMaquinaEquipamento", "ValorTotal", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PropostaLCMaquinaEquipamento", "ValorTotal");
            DropColumn("dbo.PropostaLCCultura", "PorcentagemFertilizanteCusto");
            DropColumn("dbo.PropostaLCBemUrbano", "ValorTotal");
            DropColumn("dbo.PropostaLCBemRural", "ValorTotal");
        }
    }
}
