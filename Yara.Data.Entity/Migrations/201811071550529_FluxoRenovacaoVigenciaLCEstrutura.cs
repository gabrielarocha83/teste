namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FluxoRenovacaoVigenciaLCEstrutura : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FluxoRenovacaoVigenciaLC",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Nivel = c.Int(nullable: false),
                        Ativo = c.Boolean(nullable: false),
                        EmpresaID = c.String(maxLength: 1, unicode: false),
                        UsuarioId = c.Guid(nullable: false),
                        UsuarioIDCriacao = c.Guid(nullable: false),
                        UsuarioIDAlteracao = c.Guid(),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Usuario", t => t.UsuarioId)
                .Index(t => t.UsuarioId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FluxoRenovacaoVigenciaLC", "UsuarioId", "dbo.Usuario");
            DropIndex("dbo.FluxoRenovacaoVigenciaLC", new[] { "UsuarioId" });
            DropTable("dbo.FluxoRenovacaoVigenciaLC");
        }
    }
}
