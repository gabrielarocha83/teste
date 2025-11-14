namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TamanhoDescricaoProcessmamento : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ProcessamentoCarteira", "Motivo", c => c.String(unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ProcessamentoCarteira", "Motivo", c => c.String(maxLength: 255, unicode: false));
        }
    }
}
