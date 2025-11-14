namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CondPagto_No_Titulo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Titulo", "CondPagto", c => c.String(maxLength: 4, unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Titulo", "CondPagto");
        }
    }
}
