namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DREGarantias : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PropostaLCGarantia", "PropostaLCDemonstrativoID", c => c.Guid());
            CreateIndex("dbo.PropostaLCGarantia", "PropostaLCDemonstrativoID");
            AddForeignKey("dbo.PropostaLCGarantia", "PropostaLCDemonstrativoID", "dbo.PropostaLCDemonstrativo", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PropostaLCGarantia", "PropostaLCDemonstrativoID", "dbo.PropostaLCDemonstrativo");
            DropIndex("dbo.PropostaLCGarantia", new[] { "PropostaLCDemonstrativoID" });
            DropColumn("dbo.PropostaLCGarantia", "PropostaLCDemonstrativoID");
        }
    }
}
