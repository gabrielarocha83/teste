namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Campo_DescricaoRestricao_PropostaLC : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PropostaLC", "DescricaoRestricao", c => c.String(maxLength: 256, unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PropostaLC", "DescricaoRestricao");
        }
    }
}
