namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEmpresaGrupoEconomico : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GrupoEconomico", "EmpresasID", c => c.String(maxLength: 1, fixedLength: true, unicode: false));
            CreateIndex("dbo.GrupoEconomico", "EmpresasID");
            AddForeignKey("dbo.GrupoEconomico", "EmpresasID", "dbo.Empresa", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GrupoEconomico", "EmpresasID", "dbo.Empresa");
            DropIndex("dbo.GrupoEconomico", new[] { "EmpresasID" });
            DropColumn("dbo.GrupoEconomico", "EmpresasID");
        }
    }
}
