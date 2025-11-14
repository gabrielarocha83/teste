namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UsuarioEmpresa : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Usuario_Empresa", "UsuarioID", "dbo.Usuario");
            DropForeignKey("dbo.Usuario_Empresa", "EmpresaID", "dbo.Empresa");
            DropIndex("dbo.Usuario_Empresa", new[] { "UsuarioID" });
            DropIndex("dbo.Usuario_Empresa", new[] { "EmpresaID" });
            AddColumn("dbo.Usuario", "EmpresasID", c => c.String(maxLength: 1, fixedLength: true, unicode: false));
            AlterColumn("dbo.Usuario", "EmpresaLogada", c => c.String(maxLength: 1, fixedLength: true, unicode: false));
            CreateIndex("dbo.Usuario", "EmpresasID");
            AddForeignKey("dbo.Usuario", "EmpresasID", "dbo.Empresa", "ID");
            DropTable("dbo.Usuario_Empresa");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Usuario_Empresa",
                c => new
                    {
                        UsuarioID = c.Guid(nullable: false),
                        EmpresaID = c.String(nullable: false, maxLength: 1, fixedLength: true, unicode: false),
                    })
                .PrimaryKey(t => new { t.UsuarioID, t.EmpresaID });
            
            DropForeignKey("dbo.Usuario", "EmpresasID", "dbo.Empresa");
            DropIndex("dbo.Usuario", new[] { "EmpresasID" });
            AlterColumn("dbo.Usuario", "EmpresaLogada", c => c.String(maxLength: 120, unicode: false));
            DropColumn("dbo.Usuario", "EmpresasID");
            CreateIndex("dbo.Usuario_Empresa", "EmpresaID");
            CreateIndex("dbo.Usuario_Empresa", "UsuarioID");
            AddForeignKey("dbo.Usuario_Empresa", "EmpresaID", "dbo.Empresa", "ID");
            AddForeignKey("dbo.Usuario_Empresa", "UsuarioID", "dbo.Usuario", "ID");
        }
    }
}
