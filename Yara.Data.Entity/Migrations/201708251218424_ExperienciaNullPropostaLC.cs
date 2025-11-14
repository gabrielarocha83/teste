namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ExperienciaNullPropostaLC : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.PropostaLC", new[] { "ExperienciaID" });
            AlterColumn("dbo.PropostaLC", "ExperienciaID", c => c.Guid());
            CreateIndex("dbo.PropostaLC", "ExperienciaID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.PropostaLC", new[] { "ExperienciaID" });
            AlterColumn("dbo.PropostaLC", "ExperienciaID", c => c.Guid(nullable: false));
            CreateIndex("dbo.PropostaLC", "ExperienciaID");
        }
    }
}
