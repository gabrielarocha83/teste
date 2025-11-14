namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GrupoEconomicoRating : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PropostaLCGrupoEconomico", "Rating", c => c.String(maxLength: 120, unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PropostaLCGrupoEconomico", "Rating");
        }
    }
}
