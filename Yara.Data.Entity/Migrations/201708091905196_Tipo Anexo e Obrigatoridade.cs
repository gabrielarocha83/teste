namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TipoAnexoeObrigatoridade : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Anexo",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Descricao = c.String(nullable: false, maxLength: 50, unicode: false),
                        Obrigatorio = c.Boolean(nullable: false),
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
           
            
            DropTable("dbo.Anexo");
        }
    }
}
