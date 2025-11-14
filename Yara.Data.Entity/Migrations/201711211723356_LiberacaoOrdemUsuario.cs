namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LiberacaoOrdemUsuario : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.LiberacaoOrdemVendaFluxo", "UsuarioID");
            AddForeignKey("dbo.LiberacaoOrdemVendaFluxo", "UsuarioID", "dbo.Usuario", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LiberacaoOrdemVendaFluxo", "UsuarioID", "dbo.Usuario");
            DropIndex("dbo.LiberacaoOrdemVendaFluxo", new[] { "UsuarioID" });
        }
    }
}
