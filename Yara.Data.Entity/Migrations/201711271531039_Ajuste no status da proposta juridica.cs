namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Ajustenostatusdapropostajuridica : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PropostaJuridico", "PropostaJuridicoStatus", c => c.String(maxLength: 2, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PropostaJuridico", "PropostaJuridicoStatus", c => c.Int(nullable: false));
        }
    }
}
