namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Campo_EmpresasID_Garantia : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ContaClienteGarantia", "EmpresasID", c => c.String(maxLength: 1, fixedLength: true, unicode: false));
            CreateIndex("dbo.ContaClienteGarantia", "EmpresasID");
            AddForeignKey("dbo.ContaClienteGarantia", "EmpresasID", "dbo.Empresa", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ContaClienteGarantia", "EmpresasID", "dbo.Empresa");
            DropIndex("dbo.ContaClienteGarantia", new[] { "EmpresasID" });
            DropColumn("dbo.ContaClienteGarantia", "EmpresasID");
        }
    }
}
