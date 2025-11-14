namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Cultura_Na_OV : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ItemOrdemVenda", "CodigoCulturaSAP", c => c.String(maxLength: 3, unicode: false));
            AddColumn("dbo.ItemOrdemVenda", "DesricaoCultura", c => c.String(maxLength: 20, unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ItemOrdemVenda", "DesricaoCultura");
            DropColumn("dbo.ItemOrdemVenda", "CodigoCulturaSAP");
        }
    }
}
