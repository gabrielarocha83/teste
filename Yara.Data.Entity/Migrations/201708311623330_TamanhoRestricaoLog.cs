namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TamanhoRestricaoLog : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.LogDivisaoRemessaLiberacao", "Restricao", c => c.String(unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.LogDivisaoRemessaLiberacao", "Restricao", c => c.String(maxLength: 120, unicode: false));
        }
    }
}
