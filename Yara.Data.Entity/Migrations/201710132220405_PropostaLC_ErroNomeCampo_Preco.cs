namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PropostaLC_ErroNomeCampo_Preco : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PropostaLCCultura", "Preco", c => c.Decimal(precision: 18, scale: 2));
            DropColumn("dbo.PropostaLCCultura", "Preço");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PropostaLCCultura", "Preço", c => c.Decimal(precision: 18, scale: 2));
            DropColumn("dbo.PropostaLCCultura", "Preco");
        }
    }
}
