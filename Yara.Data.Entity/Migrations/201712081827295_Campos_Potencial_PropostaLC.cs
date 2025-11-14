namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Campos_Potencial_PropostaLC : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PropostaLC", "PotencialPatrimonial", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.PropostaLC", "PotencialReceita", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PropostaLC", "PotencialReceita");
            DropColumn("dbo.PropostaLC", "PotencialPatrimonial");
        }
    }
}
