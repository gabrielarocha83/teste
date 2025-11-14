namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DataGarantiaCriacao : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PropostaLCGarantia", "DataCriacao", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PropostaLCGarantia", "DataCriacao");
        }
    }
}
