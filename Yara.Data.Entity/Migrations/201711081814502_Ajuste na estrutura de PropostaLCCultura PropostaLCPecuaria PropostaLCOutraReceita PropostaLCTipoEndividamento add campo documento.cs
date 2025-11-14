namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AjustenaestruturadePropostaLCCulturaPropostaLCPecuariaPropostaLCOutraReceitaPropostaLCTipoEndividamentoaddcampodocumento : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.PropostaLCPecuaria", new[] { "PropostaLCID" });
            DropIndex("dbo.PropostaLCCultura", new[] { "PropostaLCID" });
            DropIndex("dbo.PropostaLCOutraReceita", new[] { "PropostaLCID" });
            DropIndex("dbo.PropostaLCTipoEndividamento", new[] { "PropostaLCID" });
            AddColumn("dbo.PropostaLCPecuaria", "Documento", c => c.String(nullable: false, maxLength: 24, unicode: false));
            AddColumn("dbo.PropostaLCCultura", "Documento", c => c.String(nullable: false, maxLength: 24, unicode: false));
            AddColumn("dbo.PropostaLCOutraReceita", "Documento", c => c.String(nullable: false, maxLength: 24, unicode: false));
            AddColumn("dbo.PropostaLCTipoEndividamento", "Documento", c => c.String(nullable: false, maxLength: 24, unicode: false));
            AlterColumn("dbo.PropostaLCPecuaria", "PropostaLCID", c => c.Guid());
            AlterColumn("dbo.PropostaLCCultura", "PropostaLCID", c => c.Guid());
            AlterColumn("dbo.PropostaLCOutraReceita", "PropostaLCID", c => c.Guid());
            AlterColumn("dbo.PropostaLCTipoEndividamento", "PropostaLCID", c => c.Guid());
            CreateIndex("dbo.PropostaLCPecuaria", "PropostaLCID");
            CreateIndex("dbo.PropostaLCCultura", "PropostaLCID");
            CreateIndex("dbo.PropostaLCOutraReceita", "PropostaLCID");
            CreateIndex("dbo.PropostaLCTipoEndividamento", "PropostaLCID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.PropostaLCTipoEndividamento", new[] { "PropostaLCID" });
            DropIndex("dbo.PropostaLCOutraReceita", new[] { "PropostaLCID" });
            DropIndex("dbo.PropostaLCCultura", new[] { "PropostaLCID" });
            DropIndex("dbo.PropostaLCPecuaria", new[] { "PropostaLCID" });
            AlterColumn("dbo.PropostaLCTipoEndividamento", "PropostaLCID", c => c.Guid(nullable: false));
            AlterColumn("dbo.PropostaLCOutraReceita", "PropostaLCID", c => c.Guid(nullable: false));
            AlterColumn("dbo.PropostaLCCultura", "PropostaLCID", c => c.Guid(nullable: false));
            AlterColumn("dbo.PropostaLCPecuaria", "PropostaLCID", c => c.Guid(nullable: false));
            DropColumn("dbo.PropostaLCTipoEndividamento", "Documento");
            DropColumn("dbo.PropostaLCOutraReceita", "Documento");
            DropColumn("dbo.PropostaLCCultura", "Documento");
            DropColumn("dbo.PropostaLCPecuaria", "Documento");
            CreateIndex("dbo.PropostaLCTipoEndividamento", "PropostaLCID");
            CreateIndex("dbo.PropostaLCOutraReceita", "PropostaLCID");
            CreateIndex("dbo.PropostaLCCultura", "PropostaLCID");
            CreateIndex("dbo.PropostaLCPecuaria", "PropostaLCID");
        }
    }
}
