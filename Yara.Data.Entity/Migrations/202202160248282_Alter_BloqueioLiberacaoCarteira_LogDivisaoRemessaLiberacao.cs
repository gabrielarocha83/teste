namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Alter_BloqueioLiberacaoCarteira_LogDivisaoRemessaLiberacao : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.BloqueioLiberacaoCarteira", "Numero", c => c.String(nullable: false, maxLength: 10, unicode: false));
            AlterColumn("dbo.LogDivisaoRemessaLiberacao", "Restricao", c => c.String(maxLength: 120, unicode: false));
            CreateIndex("dbo.PropostaRenovacaoVigenciaLCCliente", "ContaClienteID");
            AddForeignKey("dbo.PropostaRenovacaoVigenciaLCCliente", "ContaClienteID", "dbo.ContaCliente", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PropostaRenovacaoVigenciaLCCliente", "ContaClienteID", "dbo.ContaCliente");
            DropIndex("dbo.PropostaRenovacaoVigenciaLCCliente", new[] { "ContaClienteID" });
            AlterColumn("dbo.LogDivisaoRemessaLiberacao", "Restricao", c => c.String(maxLength: 1000, unicode: false));
            AlterColumn("dbo.BloqueioLiberacaoCarteira", "Numero", c => c.String(maxLength: 120, unicode: false));
        }
    }
}
