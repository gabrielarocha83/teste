namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddContaClienteSolicitenteFluxo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SolicitanteFluxo", "ContaClienteID", c => c.Guid(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SolicitanteFluxo", "ContaClienteID");
        }
    }
}
