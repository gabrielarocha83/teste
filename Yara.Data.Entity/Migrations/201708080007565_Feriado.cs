namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Feriado : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Feriado",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        DataFeriado = c.DateTime(nullable: false),
                        Descricao = c.String(nullable: false, maxLength: 50, unicode: false),
                        UsuarioIDCriacao = c.Guid(nullable: false),
                        UsuarioIDAlteracao = c.Guid(),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Feriado");
        }
    }
}
