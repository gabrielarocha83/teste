namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AjustePropostaJuridico : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PropostaJuridico", "ContaClienteID", c => c.Guid());
            AlterColumn("dbo.PropostaJuridico", "ResponsavelID", c => c.Guid());
            CreateIndex("dbo.PropostaJuridico", "ContaClienteID");
            AddForeignKey("dbo.PropostaJuridico", "ContaClienteID", "dbo.ContaCliente", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PropostaJuridico", "ContaClienteID", "dbo.ContaCliente");
            DropIndex("dbo.PropostaJuridico", new[] { "ContaClienteID" });
            AlterColumn("dbo.PropostaJuridico", "ResponsavelID", c => c.Guid(nullable: false));
            DropColumn("dbo.PropostaJuridico", "ContaClienteID");
        }
    }
}
