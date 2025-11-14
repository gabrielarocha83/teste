namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddValorTotalPropostaAbono : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PropostaAbono", "ValorTotalDocumento", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PropostaAbono", "ValorTotalDocumento");
        }
    }
}
