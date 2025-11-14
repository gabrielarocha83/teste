namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AjusteTituloAddCampoPropostaStatus : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Titulo", "PropostaStatus", c => c.String(maxLength: 1, fixedLength: true, unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Titulo", "PropostaStatus");
        }
    }
}
