namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ContaCliente_RepresentanteAddEmpresaId : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ContaCliente_Representantes", newName: "ContaCliente_Representante");
            DropPrimaryKey("dbo.ContaCliente_Representante");
            AddColumn("dbo.ContaCliente_Representante", "EmpresasID", c => c.String(nullable: false, maxLength: 1, fixedLength: true, unicode: false));
            AddPrimaryKey("dbo.ContaCliente_Representante", new[] { "ContaClienteID", "RepresentanteID", "EmpresasID" });
            CreateIndex("dbo.ContaCliente_Representante", "EmpresasID");
            AddForeignKey("dbo.ContaCliente_Representante", "EmpresasID", "dbo.Empresa", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ContaCliente_Representante", "EmpresasID", "dbo.Empresa");
            DropIndex("dbo.ContaCliente_Representante", new[] { "EmpresasID" });
            DropPrimaryKey("dbo.ContaCliente_Representante");
            DropColumn("dbo.ContaCliente_Representante", "EmpresasID");
            AddPrimaryKey("dbo.ContaCliente_Representante", new[] { "ContaClienteID", "RepresentanteID" });
            RenameTable(name: "dbo.ContaCliente_Representante", newName: "ContaCliente_Representantes");
        }
    }
}
