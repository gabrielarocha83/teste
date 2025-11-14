namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DataAlcadaNull : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PropostaAlcadaComercial", "DataFundacao", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PropostaAlcadaComercial", "DataFundacao", c => c.DateTime(nullable: false));
        }
    }
}
