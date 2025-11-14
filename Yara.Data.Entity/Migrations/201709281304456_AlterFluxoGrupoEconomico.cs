namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterFluxoGrupoEconomico : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.FluxoGrupoEconomico", "UsuarioId", "dbo.Usuario");
            DropIndex("dbo.FluxoGrupoEconomico", new[] { "UsuarioId" });
            AddColumn("dbo.FluxoGrupoEconomico", "PerfilId", c => c.Guid(nullable: false));
            CreateIndex("dbo.FluxoGrupoEconomico", "PerfilId");
            AddForeignKey("dbo.FluxoGrupoEconomico", "PerfilId", "dbo.Perfil", "ID");
            DropColumn("dbo.FluxoGrupoEconomico", "UsuarioId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.FluxoGrupoEconomico", "UsuarioId", c => c.Guid(nullable: false));
            DropForeignKey("dbo.FluxoGrupoEconomico", "PerfilId", "dbo.Perfil");
            DropIndex("dbo.FluxoGrupoEconomico", new[] { "PerfilId" });
            DropColumn("dbo.FluxoGrupoEconomico", "PerfilId");
            CreateIndex("dbo.FluxoGrupoEconomico", "UsuarioId");
            AddForeignKey("dbo.FluxoGrupoEconomico", "UsuarioId", "dbo.Usuario", "ID");
        }
    }
}
