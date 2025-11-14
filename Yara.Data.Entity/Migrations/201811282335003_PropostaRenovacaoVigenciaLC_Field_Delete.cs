namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PropostaRenovacaoVigenciaLC_Field_Delete : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.PropostaRenovacaoVigenciaLC", "ResponsavelNome");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PropostaRenovacaoVigenciaLC", "ResponsavelNome", c => c.String(maxLength: 120, unicode: false));
        }
    }
}
