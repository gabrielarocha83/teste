namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserSerasa : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SolicitanteSerasa", "UsuarioID", c => c.String(maxLength: 120, unicode: false));
            AddColumn("dbo.SolicitanteSerasa", "Usuario_ID", c => c.Guid());
            CreateIndex("dbo.SolicitanteSerasa", "Usuario_ID");
            AddForeignKey("dbo.SolicitanteSerasa", "Usuario_ID", "dbo.Usuario", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SolicitanteSerasa", "Usuario_ID", "dbo.Usuario");
            DropIndex("dbo.SolicitanteSerasa", new[] { "Usuario_ID" });
            DropColumn("dbo.SolicitanteSerasa", "Usuario_ID");
            DropColumn("dbo.SolicitanteSerasa", "UsuarioID");
        }
    }
}
