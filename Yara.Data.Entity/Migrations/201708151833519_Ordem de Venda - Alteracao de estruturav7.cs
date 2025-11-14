namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrdemdeVendaAlteracaodeestruturav7 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.DivisaoRemessa");
            AlterColumn("dbo.DivisaoRemessa", "Divisao", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.DivisaoRemessa", new[] { "Divisao", "ItemOrdemVendaItem", "OrdemVendaNumero" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.DivisaoRemessa");
            AlterColumn("dbo.DivisaoRemessa", "Divisao", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.DivisaoRemessa", "Divisao");
        }
    }
}
