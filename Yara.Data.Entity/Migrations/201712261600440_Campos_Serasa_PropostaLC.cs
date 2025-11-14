namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Campos_Serasa_PropostaLC : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PropostaLC", "TipoSerasaID", c => c.Int(nullable: false));
            AddColumn("dbo.PropostaLC", "SolicitanteSerasaConjugeID", c => c.Guid());
            AddColumn("dbo.PropostaLC", "TipoSerasaConjugeID", c => c.Int(nullable: false));
            AddColumn("dbo.PropostaLCParceriaAgricola", "SolicitanteSerasaID", c => c.Guid());
            AddColumn("dbo.PropostaLCParceriaAgricola", "TipoSerasaID", c => c.Int(nullable: false));
            AddColumn("dbo.PropostaLCParceriaAgricola", "RestricaoSerasa", c => c.Boolean(nullable: false));
            CreateIndex("dbo.PropostaLC", "SolicitanteSerasaConjugeID");
            CreateIndex("dbo.PropostaLCParceriaAgricola", "SolicitanteSerasaID");
            AddForeignKey("dbo.PropostaLCParceriaAgricola", "SolicitanteSerasaID", "dbo.SolicitanteSerasa", "ID");
            AddForeignKey("dbo.PropostaLC", "SolicitanteSerasaConjugeID", "dbo.SolicitanteSerasa", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PropostaLC", "SolicitanteSerasaConjugeID", "dbo.SolicitanteSerasa");
            DropForeignKey("dbo.PropostaLCParceriaAgricola", "SolicitanteSerasaID", "dbo.SolicitanteSerasa");
            DropIndex("dbo.PropostaLCParceriaAgricola", new[] { "SolicitanteSerasaID" });
            DropIndex("dbo.PropostaLC", new[] { "SolicitanteSerasaConjugeID" });
            DropColumn("dbo.PropostaLCParceriaAgricola", "RestricaoSerasa");
            DropColumn("dbo.PropostaLCParceriaAgricola", "TipoSerasaID");
            DropColumn("dbo.PropostaLCParceriaAgricola", "SolicitanteSerasaID");
            DropColumn("dbo.PropostaLC", "TipoSerasaConjugeID");
            DropColumn("dbo.PropostaLC", "SolicitanteSerasaConjugeID");
            DropColumn("dbo.PropostaLC", "TipoSerasaID");
        }
    }
}
