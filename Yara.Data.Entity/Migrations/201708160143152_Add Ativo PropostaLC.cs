namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAtivoPropostaLC : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PropostaLCCultura", "Ativo", c => c.Boolean(nullable: false));
            AddColumn("dbo.PropostaLCGarantia", "Ativo", c => c.Boolean(nullable: false));
            AddColumn("dbo.PropostaLCNecessidadeProduto", "Ativo", c => c.Boolean(nullable: false));
            AddColumn("dbo.PropostaLCPecuaria", "Ativo", c => c.Boolean(nullable: false));
            AddColumn("dbo.PropostaLCOutrasReceitas", "Ativo", c => c.Boolean(nullable: false));
            AddColumn("dbo.PropostaLCReferencia", "Ativo", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PropostaLCReferencia", "Ativo");
            DropColumn("dbo.PropostaLCOutrasReceitas", "Ativo");
            DropColumn("dbo.PropostaLCPecuaria", "Ativo");
            DropColumn("dbo.PropostaLCNecessidadeProduto", "Ativo");
            DropColumn("dbo.PropostaLCGarantia", "Ativo");
            DropColumn("dbo.PropostaLCCultura", "Ativo");
        }
    }
}
