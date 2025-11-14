namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Campo_TokenID_Usuario : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Usuario", "TokenID", c => c.Guid());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Usuario", "TokenID");
        }
    }
}
