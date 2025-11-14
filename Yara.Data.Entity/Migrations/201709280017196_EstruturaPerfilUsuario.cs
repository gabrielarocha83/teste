namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EstruturaPerfilUsuario : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EstruturaPerfilUsuario",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        CodigoSap = c.String(nullable: false, maxLength: 10, unicode: false),
                        PerfilId = c.Guid(nullable: false),
                        UsuarioId = c.Guid(nullable: false),
                        UsuarioIDCriacao = c.Guid(nullable: false),
                        UsuarioIDAlteracao = c.Guid(),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.EstruturaComercial", t => t.CodigoSap)
                .ForeignKey("dbo.Perfil", t => t.PerfilId)
                .ForeignKey("dbo.Usuario", t => t.UsuarioId)
                .Index(t => t.CodigoSap)
                .Index(t => t.PerfilId)
                .Index(t => t.UsuarioId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EstruturaPerfilUsuario", "UsuarioId", "dbo.Usuario");
            DropForeignKey("dbo.EstruturaPerfilUsuario", "PerfilId", "dbo.Perfil");
            DropForeignKey("dbo.EstruturaPerfilUsuario", "CodigoSap", "dbo.EstruturaComercial");
            DropIndex("dbo.EstruturaPerfilUsuario", new[] { "UsuarioId" });
            DropIndex("dbo.EstruturaPerfilUsuario", new[] { "PerfilId" });
            DropIndex("dbo.EstruturaPerfilUsuario", new[] { "CodigoSap" });
            DropTable("dbo.EstruturaPerfilUsuario");
        }
    }
}
