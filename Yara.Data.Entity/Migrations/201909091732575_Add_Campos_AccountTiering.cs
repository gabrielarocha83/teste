namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Campos_AccountTiering : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ContaCliente", "Segmentacao", c => c.String(maxLength: 255, unicode: false));
            AddColumn("dbo.ContaCliente", "Categorizacao", c => c.String(maxLength: 255, unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ContaCliente", "Categorizacao");
            DropColumn("dbo.ContaCliente", "Segmentacao");
        }
    }
}
