namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddcamposPropostaLC : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PropostaLC", "ReceitaTotal", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.PropostaLC", "BalancoAuditado", c => c.Boolean(nullable: false));
            AddColumn("dbo.PropostaLC", "EmpresaAuditora", c => c.String(maxLength: 120, unicode: false));
            AddColumn("dbo.PropostaLC", "Ressalvas", c => c.Boolean(nullable: false));
            AddColumn("dbo.PropostaLC", "DemonstrativoFinanceiroID", c => c.Guid(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PropostaLC", "DemonstrativoFinanceiroID");
            DropColumn("dbo.PropostaLC", "Ressalvas");
            DropColumn("dbo.PropostaLC", "EmpresaAuditora");
            DropColumn("dbo.PropostaLC", "BalancoAuditado");
            DropColumn("dbo.PropostaLC", "ReceitaTotal");
        }
    }
}
