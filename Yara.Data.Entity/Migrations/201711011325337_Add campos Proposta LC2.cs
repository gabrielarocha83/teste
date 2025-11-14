namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddcamposPropostaLC2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PropostaLC", "DemonstrativoFinanceiroID", c => c.Guid());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PropostaLC", "DemonstrativoFinanceiroID", c => c.Guid(nullable: false));
        }
    }
}
