namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FeriadoFormatoDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Feriado", "Ativo", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Feriado", "DataFeriado", c => c.DateTime(nullable: false, storeType: "date"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Feriado", "DataFeriado", c => c.DateTime(nullable: false));
            DropColumn("dbo.Feriado", "Ativo");
        }
    }
}
