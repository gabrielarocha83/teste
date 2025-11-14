namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ParametroSistemaAddEmpresaId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ParametroSistema", "EmpresasID", c => c.String(maxLength: 1, fixedLength: true, unicode: false));
            CreateIndex("dbo.ParametroSistema", "EmpresasID");
            AddForeignKey("dbo.ParametroSistema", "EmpresasID", "dbo.Empresa", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ParametroSistema", "EmpresasID", "dbo.Empresa");
            DropIndex("dbo.ParametroSistema", new[] { "EmpresasID" });
            DropColumn("dbo.ParametroSistema", "EmpresasID");
        }
    }
}
