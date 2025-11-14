namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CamposGrupoProposta : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PropostaLCGrupoEconomico", "PotencialCredito", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.PropostaLCGrupoEconomico", "PotencialReceita", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.PropostaLCGrupoEconomico", "LimiteSugerido", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.PropostaLCGrupoEconomico", "VigenciaSugerida", c => c.DateTime());
            AddColumn("dbo.PropostaLCGrupoEconomico", "VigenciaFimSugerida", c => c.DateTime());
            CreateIndex("dbo.PropostaLCGrupoEconomico", "PropostaLCID");
            CreateIndex("dbo.PropostaLCGrupoEconomico", "DemonstrativoID");
            AddForeignKey("dbo.PropostaLCGrupoEconomico", "DemonstrativoID", "dbo.PropostaLCDemonstrativo", "ID");
            AddForeignKey("dbo.PropostaLCGrupoEconomico", "PropostaLCID", "dbo.PropostaLC", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PropostaLCGrupoEconomico", "PropostaLCID", "dbo.PropostaLC");
            DropForeignKey("dbo.PropostaLCGrupoEconomico", "DemonstrativoID", "dbo.PropostaLCDemonstrativo");
            DropIndex("dbo.PropostaLCGrupoEconomico", new[] { "DemonstrativoID" });
            DropIndex("dbo.PropostaLCGrupoEconomico", new[] { "PropostaLCID" });
            DropColumn("dbo.PropostaLCGrupoEconomico", "VigenciaFimSugerida");
            DropColumn("dbo.PropostaLCGrupoEconomico", "VigenciaSugerida");
            DropColumn("dbo.PropostaLCGrupoEconomico", "LimiteSugerido");
            DropColumn("dbo.PropostaLCGrupoEconomico", "PotencialReceita");
            DropColumn("dbo.PropostaLCGrupoEconomico", "PotencialCredito");
        }
    }
}
