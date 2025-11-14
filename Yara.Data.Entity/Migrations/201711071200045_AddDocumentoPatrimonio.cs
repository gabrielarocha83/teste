namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDocumentoPatrimonio : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PropostaLCBemRural", "Documento", c => c.String(maxLength: 120, unicode: false));
            AddColumn("dbo.PropostaLCBemUrbano", "Documento", c => c.String(maxLength: 120, unicode: false));
            AddColumn("dbo.PropostaLCMaquinaEquipamento", "Documento", c => c.String(maxLength: 120, unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PropostaLCMaquinaEquipamento", "Documento");
            DropColumn("dbo.PropostaLCBemUrbano", "Documento");
            DropColumn("dbo.PropostaLCBemRural", "Documento");
        }
    }
}
