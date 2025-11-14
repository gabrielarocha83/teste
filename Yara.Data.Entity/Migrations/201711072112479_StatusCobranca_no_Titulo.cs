namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StatusCobranca_no_Titulo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Titulo", "StatusCobrancaID", c => c.Guid());
            CreateIndex("dbo.Titulo", "StatusCobrancaID");
            AddForeignKey("dbo.Titulo", "StatusCobrancaID", "dbo.StatusCobranca", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Titulo", "StatusCobrancaID", "dbo.StatusCobranca");
            DropIndex("dbo.Titulo", new[] { "StatusCobrancaID" });
            DropColumn("dbo.Titulo", "StatusCobrancaID");
        }
    }
}
