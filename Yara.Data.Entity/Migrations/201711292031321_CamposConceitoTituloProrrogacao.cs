namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CamposConceitoTituloProrrogacao : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PropostaProrrogacaoTitulo", "NaoCobranca", c => c.Boolean(nullable: false));
            AddColumn("dbo.PropostaProrrogacaoTitulo", "ContaClienteID", c => c.Guid(nullable: false));
            DropColumn("dbo.PropostaProrrogacao", "ConceitoCobranca");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PropostaProrrogacao", "ConceitoCobranca", c => c.Boolean(nullable: false));
            DropColumn("dbo.PropostaProrrogacaoTitulo", "ContaClienteID");
            DropColumn("dbo.PropostaProrrogacaoTitulo", "NaoCobranca");
        }
    }
}
