namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DateLiberacaoFluxo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LiberacaoGrupoEconomicoFluxo", "UsuarioIDCriacao", c => c.Guid(nullable: false));
            AddColumn("dbo.LiberacaoGrupoEconomicoFluxo", "UsuarioIDAlteracao", c => c.Guid());
            AddColumn("dbo.LiberacaoGrupoEconomicoFluxo", "DataCriacao", c => c.DateTime(nullable: false));
            AddColumn("dbo.LiberacaoGrupoEconomicoFluxo", "DataAlteracao", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.LiberacaoGrupoEconomicoFluxo", "DataAlteracao");
            DropColumn("dbo.LiberacaoGrupoEconomicoFluxo", "DataCriacao");
            DropColumn("dbo.LiberacaoGrupoEconomicoFluxo", "UsuarioIDAlteracao");
            DropColumn("dbo.LiberacaoGrupoEconomicoFluxo", "UsuarioIDCriacao");
        }
    }
}
