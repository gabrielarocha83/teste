namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NullUsuarioIDLiberacaoFluxo : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.LiberacaoGrupoEconomicoFluxo", new[] { "UsuarioID" });
            AlterColumn("dbo.LiberacaoGrupoEconomicoFluxo", "UsuarioID", c => c.Guid());
            CreateIndex("dbo.LiberacaoGrupoEconomicoFluxo", "UsuarioID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.LiberacaoGrupoEconomicoFluxo", new[] { "UsuarioID" });
            AlterColumn("dbo.LiberacaoGrupoEconomicoFluxo", "UsuarioID", c => c.Guid(nullable: false));
            CreateIndex("dbo.LiberacaoGrupoEconomicoFluxo", "UsuarioID");
        }
    }
}
