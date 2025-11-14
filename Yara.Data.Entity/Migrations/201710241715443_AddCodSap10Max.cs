namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCodSap10Max : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PropostaLC", "CodigoSap", c => c.String(maxLength: 10, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PropostaLC", "CodigoSap", c => c.String(maxLength: 120, unicode: false));
        }
    }
}
