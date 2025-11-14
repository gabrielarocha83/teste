namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TipoSerasaPropostaAlcada : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PropostaAlcadaComercial", "TipoSerasaID", c => c.Int(nullable: false));
            AddColumn("dbo.PropostaAlcadaComercial", "TipoSerasaConjugeID", c => c.Int(nullable: false));
            AddColumn("dbo.PropostaAlcadaComercialDocumento", "TipoSerasaID", c => c.Int(nullable: false));
            AddColumn("dbo.PropostaAlcadaComercialParceriaAgricola", "TipoSerasaID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PropostaAlcadaComercialParceriaAgricola", "TipoSerasaID");
            DropColumn("dbo.PropostaAlcadaComercialDocumento", "TipoSerasaID");
            DropColumn("dbo.PropostaAlcadaComercial", "TipoSerasaConjugeID");
            DropColumn("dbo.PropostaAlcadaComercial", "TipoSerasaID");
        }
    }
}
