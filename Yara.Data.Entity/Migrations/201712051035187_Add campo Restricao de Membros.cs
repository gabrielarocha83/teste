namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddcampoRestricaodeMembros : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ContaClienteFinanceiro", "GrupoEconomicoRestricao", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ContaClienteFinanceiro", "GrupoEconomicoRestricao");
        }
    }
}
