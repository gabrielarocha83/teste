namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Campos_Serasa_PropostaLC_2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PropostaLC", "RestricaoSerasaFlag", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PropostaLC", "RestricaoSerasaFlag");
        }
    }
}
