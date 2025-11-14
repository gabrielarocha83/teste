namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EstruturadeFluxoLiberacaoLimiteCredito2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FluxoLiberacaoLimiteCredito", "Ativo", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.FluxoLiberacaoLimiteCredito", "Ativo");
        }
    }
}
