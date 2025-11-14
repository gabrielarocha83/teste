namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EstruturaUsuario : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UsuarioEstruturaComercial",
                c => new
                    {
                        Usuario_ID = c.Guid(nullable: false),
                        EstruturaComercial_CodigoSap = c.String(nullable: false, maxLength: 10, unicode: false),
                    })
                .PrimaryKey(t => new { t.Usuario_ID, t.EstruturaComercial_CodigoSap })
                .ForeignKey("dbo.Usuario", t => t.Usuario_ID)
                .ForeignKey("dbo.EstruturaComercial", t => t.EstruturaComercial_CodigoSap)
                .Index(t => t.Usuario_ID)
                .Index(t => t.EstruturaComercial_CodigoSap);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UsuarioEstruturaComercial", "EstruturaComercial_CodigoSap", "dbo.EstruturaComercial");
            DropForeignKey("dbo.UsuarioEstruturaComercial", "Usuario_ID", "dbo.Usuario");
            DropIndex("dbo.UsuarioEstruturaComercial", new[] { "EstruturaComercial_CodigoSap" });
            DropIndex("dbo.UsuarioEstruturaComercial", new[] { "Usuario_ID" });
            DropTable("dbo.UsuarioEstruturaComercial");
        }
    }
}
