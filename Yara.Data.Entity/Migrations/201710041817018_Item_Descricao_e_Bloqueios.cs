namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Item_Descricao_e_Bloqueios : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ItemOrdemVenda", "DescricaoMaterial", c => c.String(maxLength: 100, unicode: false));
            AddColumn("dbo.ItemOrdemVenda", "OutrosBloqueios", c => c.String(maxLength: 255, unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ItemOrdemVenda", "OutrosBloqueios");
            DropColumn("dbo.ItemOrdemVenda", "DescricaoMaterial");
        }
    }
}
