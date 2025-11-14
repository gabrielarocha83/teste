namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddcamposparabasedefixacaodeLCnaproposta : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PropostaLC", "FixarLimiteCredito", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.PropostaLC", "LimiteCreditoAnterior", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PropostaLC", "LimiteCreditoAnterior");
            DropColumn("dbo.PropostaLC", "FixarLimiteCredito");
        }
    }
}
