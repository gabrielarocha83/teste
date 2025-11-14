namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrdemVendaAddDatadeValidade : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrdemVenda", "DataValidade", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrdemVenda", "DataValidade");
        }
    }
}
