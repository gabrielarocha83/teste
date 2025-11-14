namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EstruturadeBuscadeBensdeClientev2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ContaClienteBuscaBens", "Patrimonio", c => c.String(unicode: false, storeType: "text"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ContaClienteBuscaBens", "Patrimonio", c => c.String(nullable: false, unicode: false, storeType: "text"));
        }
    }
}
