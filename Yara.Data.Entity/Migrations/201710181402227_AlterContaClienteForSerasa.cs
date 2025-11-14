namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterContaClienteForSerasa : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ContaCliente", "PendenciaSerasa", c => c.Int(nullable: false));
            AddColumn("dbo.ContaCliente", "SolicitanteSerasaID", c => c.Guid(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ContaCliente", "SolicitanteSerasaID");
            DropColumn("dbo.ContaCliente", "PendenciaSerasa");
        }
    }
}
