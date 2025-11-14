namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Campo_MotivoB7_DivisaoRemessa : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DivisaoRemessa", "MotivoB7", c => c.String(maxLength: 10, unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DivisaoRemessa", "MotivoB7");
        }
    }
}
