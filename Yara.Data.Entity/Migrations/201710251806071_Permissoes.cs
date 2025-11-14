namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Permissoes : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Grupo_Permissao", "PermissaoID", "dbo.Permissao");
            DropIndex("dbo.Grupo_Permissao", new[] { "PermissaoID" });
            DropPrimaryKey("dbo.Permissao");
            DropPrimaryKey("dbo.Grupo_Permissao");
            AddColumn("dbo.Permissao", "Processo", c => c.String(maxLength: 100, unicode: false));
            AlterColumn("dbo.Permissao", "Nome", c => c.String(nullable: false, maxLength: 50, unicode: false));
            AlterColumn("dbo.Permissao", "Descricao", c => c.String(maxLength: 100, unicode: false));
            AlterColumn("dbo.Grupo_Permissao", "PermissaoID", c => c.String(nullable: false, maxLength: 50, unicode: false));
            AddPrimaryKey("dbo.Permissao", "Nome");
            AddPrimaryKey("dbo.Grupo_Permissao", new[] { "GrupoID", "PermissaoID" });
            CreateIndex("dbo.Grupo_Permissao", "PermissaoID");
            AddForeignKey("dbo.Grupo_Permissao", "PermissaoID", "dbo.Permissao", "Nome");
            DropColumn("dbo.Permissao", "ID");
            DropColumn("dbo.Permissao", "DataCriacao");
            DropColumn("dbo.Permissao", "DataAlteracao");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Permissao", "DataAlteracao", c => c.DateTime());
            AddColumn("dbo.Permissao", "DataCriacao", c => c.DateTime(nullable: false));
            AddColumn("dbo.Permissao", "ID", c => c.Guid(nullable: false));
            DropForeignKey("dbo.Grupo_Permissao", "PermissaoID", "dbo.Permissao");
            DropIndex("dbo.Grupo_Permissao", new[] { "PermissaoID" });
            DropPrimaryKey("dbo.Grupo_Permissao");
            DropPrimaryKey("dbo.Permissao");
            AlterColumn("dbo.Grupo_Permissao", "PermissaoID", c => c.Guid(nullable: false));
            AlterColumn("dbo.Permissao", "Descricao", c => c.String(maxLength: 120, unicode: false));
            AlterColumn("dbo.Permissao", "Nome", c => c.String(maxLength: 120, unicode: false));
            DropColumn("dbo.Permissao", "Processo");
            AddPrimaryKey("dbo.Grupo_Permissao", new[] { "GrupoID", "PermissaoID" });
            AddPrimaryKey("dbo.Permissao", "ID");
            CreateIndex("dbo.Grupo_Permissao", "PermissaoID");
            AddForeignKey("dbo.Grupo_Permissao", "PermissaoID", "dbo.Permissao", "ID");
        }
    }
}
