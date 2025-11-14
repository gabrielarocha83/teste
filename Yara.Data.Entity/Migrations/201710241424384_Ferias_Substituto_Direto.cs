namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Ferias_Substituto_Direto : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.FeriasSubstituto", "FeriasID", "dbo.Ferias");
            DropForeignKey("dbo.FeriasSubstituto", "GrupoID", "dbo.Grupo");
            DropForeignKey("dbo.FeriasSubstituto", "UsuarioID", "dbo.Usuario");
            DropIndex("dbo.FeriasSubstituto", new[] { "FeriasID" });
            DropIndex("dbo.FeriasSubstituto", new[] { "UsuarioID" });
            DropIndex("dbo.FeriasSubstituto", new[] { "GrupoID" });
            AddColumn("dbo.Ferias", "SubstitutoID", c => c.Guid(nullable: false));
            CreateIndex("dbo.Ferias", "SubstitutoID");
            AddForeignKey("dbo.Ferias", "SubstitutoID", "dbo.Usuario", "ID");
            DropTable("dbo.FeriasSubstituto");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.FeriasSubstituto",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        FeriasID = c.Guid(nullable: false),
                        UsuarioID = c.Guid(nullable: false),
                        GrupoID = c.Guid(nullable: false),
                        UsuarioIDCriacao = c.Guid(nullable: false),
                        UsuarioIDAlteracao = c.Guid(),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            DropForeignKey("dbo.Ferias", "SubstitutoID", "dbo.Usuario");
            DropIndex("dbo.Ferias", new[] { "SubstitutoID" });
            DropColumn("dbo.Ferias", "SubstitutoID");
            CreateIndex("dbo.FeriasSubstituto", "GrupoID");
            CreateIndex("dbo.FeriasSubstituto", "UsuarioID");
            CreateIndex("dbo.FeriasSubstituto", "FeriasID");
            AddForeignKey("dbo.FeriasSubstituto", "UsuarioID", "dbo.Usuario", "ID");
            AddForeignKey("dbo.FeriasSubstituto", "GrupoID", "dbo.Grupo", "ID");
            AddForeignKey("dbo.FeriasSubstituto", "FeriasID", "dbo.Ferias", "ID");
        }
    }
}
