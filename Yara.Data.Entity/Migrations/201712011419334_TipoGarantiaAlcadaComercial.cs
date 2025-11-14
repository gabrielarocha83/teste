namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TipoGarantiaAlcadaComercial : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PropostaAlcadaComercial", "TipoGarantiaID", c => c.Guid());
            CreateIndex("dbo.PropostaAlcadaComercial", "TipoGarantiaID");
            AddForeignKey("dbo.PropostaAlcadaComercial", "TipoGarantiaID", "dbo.TipoGarantia", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PropostaAlcadaComercial", "TipoGarantiaID", "dbo.TipoGarantia");
            DropIndex("dbo.PropostaAlcadaComercial", new[] { "TipoGarantiaID" });
            DropColumn("dbo.PropostaAlcadaComercial", "TipoGarantiaID");
        }
    }
}
