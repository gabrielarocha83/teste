namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLCAdicionalVigenciasInicioeFim2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ContaClienteFinanceiro", "LC", c => c.Single());
            AlterColumn("dbo.ContaClienteFinanceiro", "LCDisponivel", c => c.Single());
            AlterColumn("dbo.ContaClienteFinanceiro", "LCAdicional", c => c.Single());
            AlterColumn("dbo.ContaClienteFinanceiro", "Exposicao", c => c.Single());
            AlterColumn("dbo.ContaClienteFinanceiro", "Vigencia", c => c.DateTime());
            AlterColumn("dbo.ContaClienteFinanceiro", "VigenciaFim", c => c.DateTime());
            AlterColumn("dbo.ContaClienteFinanceiro", "VigenciaAdicional", c => c.DateTime());
            AlterColumn("dbo.ContaClienteFinanceiro", "VigenciaAdicionalFim", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ContaClienteFinanceiro", "VigenciaAdicionalFim", c => c.DateTime(nullable: false));
            AlterColumn("dbo.ContaClienteFinanceiro", "VigenciaAdicional", c => c.DateTime(nullable: false));
            AlterColumn("dbo.ContaClienteFinanceiro", "VigenciaFim", c => c.DateTime(nullable: false));
            AlterColumn("dbo.ContaClienteFinanceiro", "Vigencia", c => c.DateTime(nullable: false));
            AlterColumn("dbo.ContaClienteFinanceiro", "Exposicao", c => c.Single(nullable: false));
            AlterColumn("dbo.ContaClienteFinanceiro", "LCAdicional", c => c.Single(nullable: false));
            AlterColumn("dbo.ContaClienteFinanceiro", "LCDisponivel", c => c.Single(nullable: false));
            AlterColumn("dbo.ContaClienteFinanceiro", "LC", c => c.Single(nullable: false));
        }
    }
}
