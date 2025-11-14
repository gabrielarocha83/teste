namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Ajusteparaaddconceitodeprorrogacao : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ContaClienteFinanceiro", "ConceitoCobrancaIDAnterior", c => c.Guid());
            AddColumn("dbo.ContaClienteFinanceiro", "ConceitoAnterior", c => c.Boolean(nullable: false));
            AddColumn("dbo.ContaClienteFinanceiro", "DescricaoConceitoAnterior", c => c.String(maxLength: 120, unicode: false));
            AddColumn("dbo.PropostaProrrogacao", "ConceitoCobranca", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PropostaProrrogacao", "ConceitoCobranca");
            DropColumn("dbo.ContaClienteFinanceiro", "DescricaoConceitoAnterior");
            DropColumn("dbo.ContaClienteFinanceiro", "ConceitoAnterior");
            DropColumn("dbo.ContaClienteFinanceiro", "ConceitoCobrancaIDAnterior");
        }
    }
}
