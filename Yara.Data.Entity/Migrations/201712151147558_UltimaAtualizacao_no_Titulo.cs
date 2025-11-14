namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UltimaAtualizacao_no_Titulo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Titulo", "DataUltimaAtualizacao", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Titulo", "DataUltimaAtualizacao");
        }
    }
}
