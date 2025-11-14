namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TipoSerasaSolicitanteContaCliente : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ContaCliente", "TipoConsultaSolicitanteSerasaID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ContaCliente", "TipoConsultaSolicitanteSerasaID");
        }
    }
}
