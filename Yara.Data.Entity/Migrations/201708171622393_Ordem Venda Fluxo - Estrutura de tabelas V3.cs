namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrdemVendaFluxoEstruturadetabelasV3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.OrdemVendaFluxo", "UsuarioID", c => c.Guid());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.OrdemVendaFluxo", "UsuarioID", c => c.Guid(nullable: false));
        }
    }
}
