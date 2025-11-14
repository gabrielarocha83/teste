namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CampoNumeroInternoAlcadaComercial : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PropostaAlcadaComercial", "NumeroInternoProposta", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PropostaAlcadaComercial", "NumeroInternoProposta");
        }
    }
}
