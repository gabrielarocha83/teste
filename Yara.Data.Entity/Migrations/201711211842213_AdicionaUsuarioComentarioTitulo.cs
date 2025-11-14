namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdicionaUsuarioComentarioTitulo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TituloComentario", "UsuarioID", c => c.Guid(nullable: false));
            CreateIndex("dbo.TituloComentario", "UsuarioID");
            AddForeignKey("dbo.TituloComentario", "UsuarioID", "dbo.Usuario", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TituloComentario", "UsuarioID", "dbo.Usuario");
            DropIndex("dbo.TituloComentario", new[] { "UsuarioID" });
            DropColumn("dbo.TituloComentario", "UsuarioID");
        }
    }
}
