namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AnexoArquivoEstruturadetabela : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AnexoArquivo",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        PropostaLCID = c.Guid(nullable: false),
                        AnexoID = c.Guid(nullable: false),
                        Arquivo = c.Binary(nullable: false),
                        NomeArquivo = c.String(nullable: false, maxLength: 120, unicode: false),
                        ExtensaoArquivo = c.String(nullable: false, maxLength: 120, unicode: false),
                        UsuarioIDCriacao = c.Guid(nullable: false),
                        UsuarioIDAlteracao = c.Guid(),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Anexo", t => t.AnexoID)
                .ForeignKey("dbo.PropostaLC", t => t.PropostaLCID)
                .Index(t => t.PropostaLCID)
                .Index(t => t.AnexoID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AnexoArquivo", "PropostaLCID", "dbo.PropostaLC");
            DropForeignKey("dbo.AnexoArquivo", "AnexoID", "dbo.Anexo");
            DropIndex("dbo.AnexoArquivo", new[] { "AnexoID" });
            DropIndex("dbo.AnexoArquivo", new[] { "PropostaLCID" });
            DropTable("dbo.AnexoArquivo");
        }
    }
}
