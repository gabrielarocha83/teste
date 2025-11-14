namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PropostaLC_Campo_RatingCliente : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PropostaLC", "RatingCliente", c => c.String(maxLength: 120, unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PropostaLC", "RatingCliente");
        }
    }
}
