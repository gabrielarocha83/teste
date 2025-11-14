namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFluxoComite : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PropostaLCComite", "FluxoLiberacaoLimiteCreditoID", c => c.Guid(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PropostaLCComite", "FluxoLiberacaoLimiteCreditoID");
        }
    }
}
