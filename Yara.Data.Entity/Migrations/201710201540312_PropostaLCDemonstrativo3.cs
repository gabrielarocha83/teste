namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PropostaLCDemonstrativo3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PropostaLC", "PropostaLCDemonstrativoID", c => c.Guid());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PropostaLC", "PropostaLCDemonstrativoID");
        }
    }
}
