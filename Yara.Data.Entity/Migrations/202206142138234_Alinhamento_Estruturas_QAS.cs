namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Alinhamento_Estruturas_QAS : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.BloqueioLiberacaoCarteira", "Numero", c => c.String(nullable: false, maxLength: 10, unicode: false));
            AlterColumn("dbo.LogDivisaoRemessaLiberacao", "Restricao", c => c.String(maxLength: 8000, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.LogDivisaoRemessaLiberacao", "Restricao", c => c.String(maxLength: 1000, unicode: false));
            AlterColumn("dbo.BloqueioLiberacaoCarteira", "Numero", c => c.String(maxLength: 120, unicode: false));
        }
    }
}
