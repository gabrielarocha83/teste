namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LogDivisaodeRemessaEstruturadatabela : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LogDivisaoRemessaLiberacao",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        OrdemVendaNumero = c.String(nullable: false, maxLength: 10, unicode: false),
                        OrdemVendaItem = c.Int(nullable: false),
                        Acao = c.String(maxLength: 2, unicode: false),
                        Restricao = c.String(maxLength: 120, unicode: false),
                        UsuarioId = c.Guid(nullable: false),
                        UsuarioIDCriacao = c.Guid(nullable: false),
                        UsuarioIDAlteracao = c.Guid(),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.LogDivisaoRemessaLiberacao");
        }
    }
}
