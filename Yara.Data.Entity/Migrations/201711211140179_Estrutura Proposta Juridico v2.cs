namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EstruturaPropostaJuridicov2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PropostaJuridico", "ComentarioHistorico", c => c.String(unicode: false, storeType: "text"));
            AlterColumn("dbo.PropostaJuridico", "ParecerVisita", c => c.String(unicode: false, storeType: "text"));
            AlterColumn("dbo.PropostaJuridico", "ParecerCobranca", c => c.String(unicode: false, storeType: "text"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PropostaJuridico", "ParecerCobranca", c => c.String(maxLength: 120, unicode: false));
            AlterColumn("dbo.PropostaJuridico", "ParecerVisita", c => c.String(maxLength: 120, unicode: false));
            AlterColumn("dbo.PropostaJuridico", "ComentarioHistorico", c => c.String(maxLength: 120, unicode: false));
        }
    }
}
