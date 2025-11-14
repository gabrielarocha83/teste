namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FK_PropostaLCComite_FluxoLiberacaoLimiteCredito : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.PropostaLCComite", "FluxoLiberacaoLimiteCreditoID");
            AddForeignKey("dbo.PropostaLCComite", "FluxoLiberacaoLimiteCreditoID", "dbo.FluxoLiberacaoLimiteCredito", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PropostaLCComite", "FluxoLiberacaoLimiteCreditoID", "dbo.FluxoLiberacaoLimiteCredito");
            DropIndex("dbo.PropostaLCComite", new[] { "FluxoLiberacaoLimiteCreditoID" });
        }
    }
}
