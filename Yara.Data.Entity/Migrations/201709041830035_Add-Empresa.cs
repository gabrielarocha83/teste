namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEmpresa : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Empresa",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 1, fixedLength: true, unicode: false),
                        Nome = c.String(maxLength: 120, unicode: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Usuario_Empresa",
                c => new
                    {
                        UsuarioID = c.Guid(nullable: false),
                        EmpresaID = c.String(nullable: false, maxLength: 1, fixedLength: true, unicode: false),
                    })
                .PrimaryKey(t => new { t.UsuarioID, t.EmpresaID })
                .ForeignKey("dbo.Usuario", t => t.UsuarioID)
                .ForeignKey("dbo.Empresa", t => t.EmpresaID)
                .Index(t => t.UsuarioID)
                .Index(t => t.EmpresaID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Usuario_Empresa", "EmpresaID", "dbo.Empresa");
            DropForeignKey("dbo.Usuario_Empresa", "UsuarioID", "dbo.Usuario");
            DropIndex("dbo.Usuario_Empresa", new[] { "EmpresaID" });
            DropIndex("dbo.Usuario_Empresa", new[] { "UsuarioID" });
            DropTable("dbo.Usuario_Empresa");
            DropTable("dbo.Empresa");
        }
    }
}
