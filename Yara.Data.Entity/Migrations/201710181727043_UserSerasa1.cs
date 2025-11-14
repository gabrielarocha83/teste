namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserSerasa1 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.SolicitanteSerasa", new[] { "Usuario_ID" });
            DropColumn("dbo.SolicitanteSerasa", "UsuarioID");
            RenameColumn(table: "dbo.SolicitanteSerasa", name: "Usuario_ID", newName: "UsuarioID");
            AlterColumn("dbo.SolicitanteSerasa", "UsuarioID", c => c.Guid(nullable: false));
            AlterColumn("dbo.SolicitanteSerasa", "UsuarioID", c => c.Guid(nullable: false));
            CreateIndex("dbo.SolicitanteSerasa", "UsuarioID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.SolicitanteSerasa", new[] { "UsuarioID" });
            AlterColumn("dbo.SolicitanteSerasa", "UsuarioID", c => c.Guid());
            AlterColumn("dbo.SolicitanteSerasa", "UsuarioID", c => c.String(maxLength: 120, unicode: false));
            RenameColumn(table: "dbo.SolicitanteSerasa", name: "UsuarioID", newName: "Usuario_ID");
            AddColumn("dbo.SolicitanteSerasa", "UsuarioID", c => c.String(maxLength: 120, unicode: false));
            CreateIndex("dbo.SolicitanteSerasa", "Usuario_ID");
        }
    }
}
