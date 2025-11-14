namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ContaClienteAlteracaodeEstruturapermitindocamposnulosv2 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.ContaCliente", new[] { "TipoClienteID" });
            AddColumn("dbo.ContaCliente", "Simplificado", c => c.Boolean(nullable: false));
            AlterColumn("dbo.ContaCliente", "CodigoPrincipal", c => c.String(maxLength: 10, unicode: false));
            AlterColumn("dbo.ContaCliente", "TipoClienteID", c => c.Guid());
            AlterColumn("dbo.ContaCliente", "BloqueioManual", c => c.Boolean());
            AlterColumn("dbo.ContaCliente", "LiberacaoManual", c => c.Boolean());
            AlterColumn("dbo.ContaCliente", "AdiantamentoLC", c => c.Boolean());
            AlterColumn("dbo.ContaCliente", "RestricaoSerasa", c => c.Boolean());
            AlterColumn("dbo.ContaCliente", "TOP10", c => c.Boolean());
            AlterColumn("dbo.ContaCliente", "ClientePremium", c => c.Boolean());
            CreateIndex("dbo.ContaCliente", "TipoClienteID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.ContaCliente", new[] { "TipoClienteID" });
            AlterColumn("dbo.ContaCliente", "ClientePremium", c => c.Boolean(nullable: false));
            AlterColumn("dbo.ContaCliente", "TOP10", c => c.Boolean(nullable: false));
            AlterColumn("dbo.ContaCliente", "RestricaoSerasa", c => c.Boolean(nullable: false));
            AlterColumn("dbo.ContaCliente", "AdiantamentoLC", c => c.Boolean(nullable: false));
            AlterColumn("dbo.ContaCliente", "LiberacaoManual", c => c.Boolean(nullable: false));
            AlterColumn("dbo.ContaCliente", "BloqueioManual", c => c.Boolean(nullable: false));
            AlterColumn("dbo.ContaCliente", "TipoClienteID", c => c.Guid(nullable: false));
            AlterColumn("dbo.ContaCliente", "CodigoPrincipal", c => c.String(nullable: false, maxLength: 10, unicode: false));
            DropColumn("dbo.ContaCliente", "Simplificado");
            CreateIndex("dbo.ContaCliente", "TipoClienteID");
        }
    }
}
