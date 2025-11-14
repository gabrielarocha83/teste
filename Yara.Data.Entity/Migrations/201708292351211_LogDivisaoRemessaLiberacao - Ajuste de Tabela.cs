namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LogDivisaoRemessaLiberacaoAjustedeTabela : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LogDivisaoRemessaLiberacao", "ProcessamentoCarteiraID", c => c.Guid(nullable: false));
            AddColumn("dbo.LogDivisaoRemessaLiberacao", "OrdemDivisao", c => c.Int(nullable: false));
            CreateIndex("dbo.LogDivisaoRemessaLiberacao", "ProcessamentoCarteiraID");
            AddForeignKey("dbo.LogDivisaoRemessaLiberacao", "ProcessamentoCarteiraID", "dbo.ProcessamentoCarteira", "ID");
            DropColumn("dbo.LogDivisaoRemessaLiberacao", "Acao");
            DropColumn("dbo.LogDivisaoRemessaLiberacao", "UsuarioId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.LogDivisaoRemessaLiberacao", "UsuarioId", c => c.Guid(nullable: false));
            AddColumn("dbo.LogDivisaoRemessaLiberacao", "Acao", c => c.String(maxLength: 2, unicode: false));
            DropForeignKey("dbo.LogDivisaoRemessaLiberacao", "ProcessamentoCarteiraID", "dbo.ProcessamentoCarteira");
            DropIndex("dbo.LogDivisaoRemessaLiberacao", new[] { "ProcessamentoCarteiraID" });
            DropColumn("dbo.LogDivisaoRemessaLiberacao", "OrdemDivisao");
            DropColumn("dbo.LogDivisaoRemessaLiberacao", "ProcessamentoCarteiraID");
        }
    }
}
