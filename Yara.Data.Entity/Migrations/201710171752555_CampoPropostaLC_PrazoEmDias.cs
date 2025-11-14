namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CampoPropostaLC_PrazoEmDias : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PropostaLC", "PrazoEmDias", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PropostaLC", "PrazoEmDias");
        }
    }
}
