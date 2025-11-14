namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ContaClienteFinanceiroTeste : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ContaClienteFinanceiro", "EmpresasID", c => c.String(maxLength: 1, fixedLength: true, unicode: false));
            CreateIndex("dbo.ContaClienteFinanceiro", "EmpresasID");
            AddForeignKey("dbo.ContaClienteFinanceiro", "EmpresasID", "dbo.Empresa", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ContaClienteFinanceiro", "EmpresasID", "dbo.Empresa");
            DropIndex("dbo.ContaClienteFinanceiro", new[] { "EmpresasID" });
            DropColumn("dbo.ContaClienteFinanceiro", "EmpresasID");
        }
    }
}
