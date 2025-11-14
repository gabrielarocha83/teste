namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterDecimalColunaCulturaEstado : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CulturaEstado", "ProdutividadeMedia", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.CulturaEstado", "PorcentagemFertilizanteCusto", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.CulturaEstado", "MediaFertilizante", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CulturaEstado", "MediaFertilizante", c => c.Int(nullable: false));
            AlterColumn("dbo.CulturaEstado", "PorcentagemFertilizanteCusto", c => c.Int(nullable: false));
            AlterColumn("dbo.CulturaEstado", "ProdutividadeMedia", c => c.Int(nullable: false));
        }
    }
}
