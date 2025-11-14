namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FluxoGrupoEconomicoAdicionadoEstruturadeTabela : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FluxoGrupoEconomico",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Nivel = c.Int(nullable: false),
                        UsuarioId = c.Guid(nullable: false),
                        Ativo = c.Boolean(nullable: false),
                        ClassificacaoGrupoEconomicoId = c.Int(nullable: false),
                        UsuarioIDCriacao = c.Guid(nullable: false),
                        UsuarioIDAlteracao = c.Guid(),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ClassificacaoGrupoEconomico", t => t.ClassificacaoGrupoEconomicoId)
                .ForeignKey("dbo.Usuario", t => t.UsuarioId)
                .Index(t => t.UsuarioId)
                .Index(t => t.ClassificacaoGrupoEconomicoId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FluxoGrupoEconomico", "UsuarioId", "dbo.Usuario");
            DropForeignKey("dbo.FluxoGrupoEconomico", "ClassificacaoGrupoEconomicoId", "dbo.ClassificacaoGrupoEconomico");
            DropIndex("dbo.FluxoGrupoEconomico", new[] { "ClassificacaoGrupoEconomicoId" });
            DropIndex("dbo.FluxoGrupoEconomico", new[] { "UsuarioId" });
            DropTable("dbo.FluxoGrupoEconomico");
        }
    }
}
