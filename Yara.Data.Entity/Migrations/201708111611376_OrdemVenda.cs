namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrdemVenda : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OrdemVenda",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        ClienteID = c.Guid(nullable: false),
                        NumeroOrdem = c.Int(nullable: false),
                        NumeroNotaFiscal = c.Int(nullable: false),
                        Titulo = c.Int(nullable: false),
                        TipoTitulo = c.String(nullable: false, maxLength: 120, unicode: false),
                        ItemOrdem = c.String(maxLength: 120, unicode: false),
                        Vencimento = c.DateTime(nullable: false),
                        MoedaDocumento = c.String(nullable: false, maxLength: 120, unicode: false),
                        ValorMoedaDocumento = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ValorReais = c.Decimal(nullable: false, precision: 18, scale: 2),
                        UsuarioIDCriacao = c.Guid(nullable: false),
                        UsuarioIDAlteracao = c.Guid(),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.OrdemVenda");
        }
    }
}
