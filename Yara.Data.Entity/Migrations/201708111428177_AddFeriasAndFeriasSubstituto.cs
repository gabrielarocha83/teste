namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFeriasAndFeriasSubstituto : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Ferias",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        UsuarioID = c.Guid(nullable: false),
                        FeriasInicio = c.DateTime(nullable: false),
                        FeriasFim = c.DateTime(nullable: false),
                        Ativo = c.Boolean(nullable: false),
                        UsuarioIDCriacao = c.Guid(nullable: false),
                        UsuarioIDAlteracao = c.Guid(),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Usuario", t => t.UsuarioID)
                .Index(t => t.UsuarioID);
            
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
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Ferias", t => t.FeriasID)
                .ForeignKey("dbo.Grupo", t => t.GrupoID)
                .ForeignKey("dbo.Usuario", t => t.UsuarioID)
                .Index(t => t.FeriasID)
                .Index(t => t.UsuarioID)
                .Index(t => t.GrupoID);
            
            AddColumn("dbo.Grupo", "IsProcesso", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Ferias", "UsuarioID", "dbo.Usuario");
            DropForeignKey("dbo.FeriasSubstituto", "UsuarioID", "dbo.Usuario");
            DropForeignKey("dbo.FeriasSubstituto", "GrupoID", "dbo.Grupo");
            DropForeignKey("dbo.FeriasSubstituto", "FeriasID", "dbo.Ferias");
            DropIndex("dbo.FeriasSubstituto", new[] { "GrupoID" });
            DropIndex("dbo.FeriasSubstituto", new[] { "UsuarioID" });
            DropIndex("dbo.FeriasSubstituto", new[] { "FeriasID" });
            DropIndex("dbo.Ferias", new[] { "UsuarioID" });
            DropColumn("dbo.Grupo", "IsProcesso");
            DropTable("dbo.FeriasSubstituto");
            DropTable("dbo.Ferias");
        }
    }
}
