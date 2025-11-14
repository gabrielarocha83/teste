namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ParametroSistema : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ParametroSistema",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Grupo = c.String(nullable: false, maxLength: 120, unicode: false),
                        Tipo = c.String(nullable: false, maxLength: 120, unicode: false),
                        Chave = c.String(nullable: false, maxLength: 120, unicode: false),
                        Valor = c.String(nullable: false, maxLength: 120, unicode: false),
                        Ativo = c.Boolean(nullable: false),
                        UsuarioIDCriacao = c.Guid(nullable: false),
                        UsuarioIDAlteracao = c.Guid(),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ParametroSistema");
        }
    }
}
