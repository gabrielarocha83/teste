namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Campo_Titulo_HistoricoV2_VariacaoCambial : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Titulo", "VariacaoCambial", c => c.Decimal(nullable: false, precision: 13, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Titulo", "VariacaoCambial");
        }
    }
}
