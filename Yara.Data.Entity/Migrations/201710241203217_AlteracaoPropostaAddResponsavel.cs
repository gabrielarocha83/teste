namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlteracaoPropostaAddResponsavel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PropostaLC", "ResponsavelID", c => c.Guid(nullable: false));
            CreateIndex("dbo.PropostaLC", "ResponsavelID");
            AddForeignKey("dbo.PropostaLC", "ResponsavelID", "dbo.Usuario", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PropostaLC", "ResponsavelID", "dbo.Usuario");
            DropIndex("dbo.PropostaLC", new[] { "ResponsavelID" });
            DropColumn("dbo.PropostaLC", "ResponsavelID");
        }
    }
}
