namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PropostaRenovacaoVigenciaLC_Fields_Length : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PropostaRenovacaoVigenciaLCCliente", "PropostaLCStatus", c => c.String(maxLength: 120, unicode: false));
            AlterColumn("dbo.PropostaRenovacaoVigenciaLCCliente", "PropostaACStatus", c => c.String(maxLength: 120, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PropostaRenovacaoVigenciaLCCliente", "PropostaACStatus", c => c.String(maxLength: 2, fixedLength: true, unicode: false));
            AlterColumn("dbo.PropostaRenovacaoVigenciaLCCliente", "PropostaLCStatus", c => c.String(maxLength: 2, fixedLength: true, unicode: false));
        }
    }
}
