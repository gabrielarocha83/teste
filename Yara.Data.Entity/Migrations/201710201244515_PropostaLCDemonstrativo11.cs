namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PropostaLCDemonstrativo11 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PropostaLCDemonstrativo", "Conteudo", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PropostaLCDemonstrativo", "Conteudo");
        }
    }
}
