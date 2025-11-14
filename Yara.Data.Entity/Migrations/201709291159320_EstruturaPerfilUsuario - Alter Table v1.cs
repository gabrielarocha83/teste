namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EstruturaPerfilUsuarioAlterTablev1 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.EstruturaPerfilUsuario", new[] { "UsuarioId" });
            AlterColumn("dbo.EstruturaPerfilUsuario", "UsuarioId", c => c.Guid());
            CreateIndex("dbo.EstruturaPerfilUsuario", "UsuarioId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.EstruturaPerfilUsuario", new[] { "UsuarioId" });
            AlterColumn("dbo.EstruturaPerfilUsuario", "UsuarioId", c => c.Guid(nullable: false));
            CreateIndex("dbo.EstruturaPerfilUsuario", "UsuarioId");
        }
    }
}
