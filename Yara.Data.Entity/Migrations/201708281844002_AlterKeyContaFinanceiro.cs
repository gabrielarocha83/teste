namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterKeyContaFinanceiro : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.ContaClienteFinanceiro", new[] { "ContaCliente_ID" });
            DropPrimaryKey("dbo.ContaClienteFinanceiro");
            DropColumn("dbo.ContaClienteFinanceiro", "ContaClienteID");
            RenameColumn(table: "dbo.ContaClienteFinanceiro", name: "ContaCliente_ID", newName: "ContaClienteID");
          
            AlterColumn("dbo.ContaClienteFinanceiro", "ContaClienteID", c => c.Guid(nullable: false));
            AddPrimaryKey("dbo.ContaClienteFinanceiro", "ContaClienteID");
            CreateIndex("dbo.ContaClienteFinanceiro", "ContaClienteID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.ContaClienteFinanceiro", new[] { "ContaClienteID" });
            DropPrimaryKey("dbo.ContaClienteFinanceiro");
            AlterColumn("dbo.ContaClienteFinanceiro", "ContaClienteID", c => c.Guid());
            AddPrimaryKey("dbo.ContaClienteFinanceiro", "ContaClienteID");
            RenameColumn(table: "dbo.ContaClienteFinanceiro", name: "ContaClienteID", newName: "ContaCliente_ID");
            AddColumn("dbo.ContaClienteFinanceiro", "ContaClienteID", c => c.Guid(nullable: false));
            CreateIndex("dbo.ContaClienteFinanceiro", "ContaCliente_ID");
        }
    }
}
