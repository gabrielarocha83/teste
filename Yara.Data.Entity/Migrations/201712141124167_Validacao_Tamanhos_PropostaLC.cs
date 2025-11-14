namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Validacao_Tamanhos_PropostaLC : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PropostaLC", "Trading", c => c.String(maxLength: 256, unicode: false));
            AlterColumn("dbo.PropostaLC", "ComentarioMercado", c => c.String(unicode: false, storeType: "text"));
            AlterColumn("dbo.PropostaLC", "FonteRecursosCarteira", c => c.String(maxLength: 256, unicode: false));
            AlterColumn("dbo.PropostaLC", "ParecerAnalista", c => c.String(unicode: false, storeType: "text"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PropostaLC", "ParecerAnalista", c => c.String(maxLength: 400, unicode: false));
            AlterColumn("dbo.PropostaLC", "FonteRecursosCarteira", c => c.String(maxLength: 128, unicode: false));
            AlterColumn("dbo.PropostaLC", "ComentarioMercado", c => c.String(maxLength: 256, unicode: false));
            AlterColumn("dbo.PropostaLC", "Trading", c => c.String(maxLength: 128, unicode: false));
        }
    }
}
