namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlteradocamposnapropostaLC : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PropostaLC", "ValorTotalBensRurais", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.PropostaLC", "ValorTotalBensUrbanos", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.PropostaLC", "ValorTotalMaquinasEquipamentos", c => c.Decimal(precision: 18, scale: 2));
            DropColumn("dbo.PropostaLCBemRural", "ValorTotal");
            DropColumn("dbo.PropostaLCBemUrbano", "ValorTotal");
            DropColumn("dbo.PropostaLCMaquinaEquipamento", "ValorTotal");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PropostaLCMaquinaEquipamento", "ValorTotal", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.PropostaLCBemUrbano", "ValorTotal", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.PropostaLCBemRural", "ValorTotal", c => c.Decimal(precision: 18, scale: 2));
            DropColumn("dbo.PropostaLC", "ValorTotalMaquinasEquipamentos");
            DropColumn("dbo.PropostaLC", "ValorTotalBensUrbanos");
            DropColumn("dbo.PropostaLC", "ValorTotalBensRurais");
        }
    }
}
