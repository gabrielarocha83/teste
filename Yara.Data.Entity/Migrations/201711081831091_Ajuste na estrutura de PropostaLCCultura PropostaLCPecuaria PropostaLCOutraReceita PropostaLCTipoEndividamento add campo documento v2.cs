namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AjustenaestruturadePropostaLCCulturaPropostaLCPecuariaPropostaLCOutraReceitaPropostaLCTipoEndividamentoaddcampodocumentov2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PropostaLCPecuaria", "Documento", c => c.String(maxLength: 24, unicode: false));
            AlterColumn("dbo.PropostaLCCultura", "Documento", c => c.String(maxLength: 24, unicode: false));
            AlterColumn("dbo.PropostaLCOutraReceita", "Documento", c => c.String(maxLength: 24, unicode: false));
            AlterColumn("dbo.PropostaLCTipoEndividamento", "Documento", c => c.String(maxLength: 24, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PropostaLCTipoEndividamento", "Documento", c => c.String(nullable: false, maxLength: 24, unicode: false));
            AlterColumn("dbo.PropostaLCOutraReceita", "Documento", c => c.String(nullable: false, maxLength: 24, unicode: false));
            AlterColumn("dbo.PropostaLCCultura", "Documento", c => c.String(nullable: false, maxLength: 24, unicode: false));
            AlterColumn("dbo.PropostaLCPecuaria", "Documento", c => c.String(nullable: false, maxLength: 24, unicode: false));
        }
    }
}
