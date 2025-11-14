namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIsAreaIrrigadaPropostaLC : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PropostaLC", "IsAreaIrrigada", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PropostaLC", "IsAreaIrrigada");
        }
    }
}
