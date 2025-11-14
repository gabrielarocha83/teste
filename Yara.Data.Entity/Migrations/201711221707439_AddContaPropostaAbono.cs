namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddContaPropostaAbono : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PropostaAbono", "ContaClienteID", c => c.Guid(nullable: false));
            CreateIndex("dbo.PropostaAbono", "ContaClienteID");
            AddForeignKey("dbo.PropostaAbono", "ContaClienteID", "dbo.ContaCliente", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PropostaAbono", "ContaClienteID", "dbo.ContaCliente");
            DropIndex("dbo.PropostaAbono", new[] { "ContaClienteID" });
            DropColumn("dbo.PropostaAbono", "ContaClienteID");
        }
    }
}
