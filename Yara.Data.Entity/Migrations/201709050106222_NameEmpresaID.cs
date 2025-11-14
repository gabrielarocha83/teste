namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NameEmpresaID : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.HistoricoContaCliente", "EmpresaID");
            RenameColumn(table: "dbo.HistoricoContaCliente", name: "Empresas_ID", newName: "EmpresasID");
            RenameIndex(table: "dbo.HistoricoContaCliente", name: "IX_Empresas_ID", newName: "IX_EmpresasID");
            
        }
        
        public override void Down()
        {
            AddColumn("dbo.HistoricoContaCliente", "EmpresaID", c => c.String(maxLength: 1, fixedLength: true, unicode: false));
            RenameIndex(table: "dbo.HistoricoContaCliente", name: "IX_EmpresasID", newName: "IX_Empresas_ID");
            RenameColumn(table: "dbo.HistoricoContaCliente", name: "EmpresasID", newName: "Empresas_ID");
        }
    }
}
