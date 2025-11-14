namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrdemdeVendaAlteracaodeestruturav3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.DivisaoRemessa", "DataSaida", c => c.DateTime());
            AlterColumn("dbo.ItemOrdemVenda", "Deposito", c => c.String(maxLength: 4, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ItemOrdemVenda", "Deposito", c => c.String(nullable: false, maxLength: 4, unicode: false));
            AlterColumn("dbo.DivisaoRemessa", "DataSaida", c => c.DateTime(nullable: false));
        }
    }
}
