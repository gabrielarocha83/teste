namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Conversao_OrdemVenda_Dolar : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ItemOrdemVenda", "ValorEmMoeda", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.ItemOrdemVenda", "CotacaoMoeda", c => c.Decimal(nullable: false, precision: 9, scale: 5));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ItemOrdemVenda", "CotacaoMoeda");
            DropColumn("dbo.ItemOrdemVenda", "ValorEmMoeda");
        }
    }
}
