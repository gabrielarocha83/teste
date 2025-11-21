namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Campo_Ecomm_PropostaLC : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PropostaLC", "Ecomm", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PropostaLC", "Ecomm");
        }
    }
}
