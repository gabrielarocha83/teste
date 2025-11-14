namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Alter_Campo_PRDias : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.HistoricoContaCliente", "PRDias", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.HistoricoContaCliente", "PRDias", c => c.Int(nullable: false));
        }
    }
}
