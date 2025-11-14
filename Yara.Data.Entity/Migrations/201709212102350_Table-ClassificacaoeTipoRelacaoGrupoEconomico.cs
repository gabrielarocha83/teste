namespace Yara.Data.Entity.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class TableClassificacaoeTipoRelacaoGrupoEconomico : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ClassificacaoGrupoEconomico",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 120, unicode: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.TipoRelacaoGrupoEconomico",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Nome = c.String(nullable: false, maxLength: 120, unicode: false),
                        ClassificacaoGrupoEconomicoID = c.Int(nullable: false),
                        Ativo = c.Boolean(nullable: false),
                        UsuarioIDCriacao = c.Guid(nullable: false),
                        UsuarioIDAlteracao = c.Guid(),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ClassificacaoGrupoEconomico", t => t.ClassificacaoGrupoEconomicoID)
                .Index(t => t.ClassificacaoGrupoEconomicoID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TipoRelacaoGrupoEconomico", "ClassificacaoGrupoEconomicoID", "dbo.ClassificacaoGrupoEconomico");
            DropIndex("dbo.TipoRelacaoGrupoEconomico", new[] { "ClassificacaoGrupoEconomicoID" });
            DropTable("dbo.TipoRelacaoGrupoEconomico");
            DropTable("dbo.ClassificacaoGrupoEconomico");
        }
    }
}
