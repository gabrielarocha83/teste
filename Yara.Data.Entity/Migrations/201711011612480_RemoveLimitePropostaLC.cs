namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveLimitePropostaLC : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.PropostaLC", "LimiteCreditoAnterior");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PropostaLC", "LimiteCreditoAnterior", c => c.Decimal(precision: 18, scale: 2));
        }
    }
}
