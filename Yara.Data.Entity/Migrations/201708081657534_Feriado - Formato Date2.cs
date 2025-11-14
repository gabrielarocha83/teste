namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FeriadoFormatoDate2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Feriado", "DataFeriado", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Feriado", "DataFeriado", c => c.DateTime(nullable: false, storeType: "date"));
        }
    }
}
