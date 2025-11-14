namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLCAdicionalVigenciasInicioeFim : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ContaClienteFinanceiro", "LCAdicional", c => c.Single(nullable: false));
            AddColumn("dbo.ContaClienteFinanceiro", "VigenciaFim", c => c.DateTime(nullable: false));
            AddColumn("dbo.ContaClienteFinanceiro", "VigenciaAdicional", c => c.DateTime(nullable: false));
            AddColumn("dbo.ContaClienteFinanceiro", "VigenciaAdicionalFim", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ContaClienteFinanceiro", "VigenciaAdicionalFim");
            DropColumn("dbo.ContaClienteFinanceiro", "VigenciaAdicional");
            DropColumn("dbo.ContaClienteFinanceiro", "VigenciaFim");
            DropColumn("dbo.ContaClienteFinanceiro", "LCAdicional");
        }
    }
}
