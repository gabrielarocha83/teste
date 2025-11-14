namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ContaClienteFinanceiroMudancaEstrutura : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.ContaClienteFinanceiro", new[] { "EmpresasID" });
            DropPrimaryKey("dbo.ContaClienteFinanceiro");
            AlterColumn("dbo.ContaClienteFinanceiro", "EmpresasID", c => c.String(nullable: false, maxLength: 1, fixedLength: true, unicode: false));
            AddPrimaryKey("dbo.ContaClienteFinanceiro", new[] { "ContaClienteID", "EmpresasID" });
            CreateIndex("dbo.ContaClienteFinanceiro", "EmpresasID");
            DropColumn("dbo.ContaClienteFinanceiro", "ClientePremium");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ContaClienteFinanceiro", "ClientePremium", c => c.Boolean(nullable: false));
            DropIndex("dbo.ContaClienteFinanceiro", new[] { "EmpresasID" });
            DropPrimaryKey("dbo.ContaClienteFinanceiro");
            AlterColumn("dbo.ContaClienteFinanceiro", "EmpresasID", c => c.String(maxLength: 1, fixedLength: true, unicode: false));
            AddPrimaryKey("dbo.ContaClienteFinanceiro", "ContaClienteID");
            CreateIndex("dbo.ContaClienteFinanceiro", "EmpresasID");
        }
    }
}
