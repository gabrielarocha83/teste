namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SerasaAlteracoes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SolicitanteSerasa", "UsuarioID", c => c.Guid(nullable: false));
            CreateIndex("dbo.SolicitanteSerasa", "UsuarioID");
            AddForeignKey("dbo.SolicitanteSerasa", "UsuarioID", "dbo.Usuario", "ID");
            DropColumn("dbo.SolicitanteSerasa", "Usuario");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SolicitanteSerasa", "Usuario", c => c.String(maxLength: 120, unicode: false));
            DropForeignKey("dbo.SolicitanteSerasa", "UsuarioID", "dbo.Usuario");
            DropIndex("dbo.SolicitanteSerasa", new[] { "UsuarioID" });
            DropColumn("dbo.SolicitanteSerasa", "UsuarioID");
        }
    }
}
