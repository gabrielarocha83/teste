namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Numeracao_PropostaLC : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PropostaLC", "NumeroInternoProposta", c => c.Int(nullable: false));
            AddColumn("dbo.SolicitanteSerasa", "Usuario", c => c.String(maxLength: 120, unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SolicitanteSerasa", "Usuario");
            DropColumn("dbo.PropostaLC", "NumeroInternoProposta");
        }
    }
}
