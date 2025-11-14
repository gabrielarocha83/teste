namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AnexoArquivoCobranca : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AnexoArquivoCobranca",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        PropostaCobranca = c.Guid(nullable: false),
                        ContaClienteID = c.Guid(),
                        TipoProposta = c.Int(nullable: false),
                        Arquivo = c.Binary(nullable: false),
                        NomeArquivo = c.String(nullable: false, maxLength: 50, unicode: false),
                        ExtensaoArquivo = c.String(nullable: false, maxLength: 6, unicode: false),
                        Ativo = c.Boolean(nullable: false),
                        UsuarioIDCriacao = c.Guid(nullable: false),
                        UsuarioIDAlteracao = c.Guid(),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ContaCliente", t => t.ContaClienteID)
                .Index(t => t.ContaClienteID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AnexoArquivoCobranca", "ContaClienteID", "dbo.ContaCliente");
            DropIndex("dbo.AnexoArquivoCobranca", new[] { "ContaClienteID" });
            DropTable("dbo.AnexoArquivoCobranca");
        }
    }
}
