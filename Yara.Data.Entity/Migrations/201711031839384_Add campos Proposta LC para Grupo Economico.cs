namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddcamposPropostaLCparaGrupoEconomico : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PropostaLC", "AnaliseGrupo", c => c.Boolean(nullable: false));
            AddColumn("dbo.PropostaLC", "GrupoEconomicoID", c => c.Guid());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PropostaLC", "GrupoEconomicoID");
            DropColumn("dbo.PropostaLC", "AnaliseGrupo");
        }
    }
}
