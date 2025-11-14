namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PropostaLCCamposNovos : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PropostaLC", "TipoProposta", c => c.String(maxLength: 120, unicode: false));
            AddColumn("dbo.PropostaLC", "TipoClienteID", c => c.Guid());
            AddColumn("dbo.PropostaLC", "SegmentoID", c => c.Guid());
            CreateIndex("dbo.PropostaLC", "TipoClienteID");
            CreateIndex("dbo.PropostaLC", "SegmentoID");
            AddForeignKey("dbo.PropostaLC", "SegmentoID", "dbo.Segmento", "ID");
            AddForeignKey("dbo.PropostaLC", "TipoClienteID", "dbo.TipoCliente", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PropostaLC", "TipoClienteID", "dbo.TipoCliente");
            DropForeignKey("dbo.PropostaLC", "SegmentoID", "dbo.Segmento");
            DropIndex("dbo.PropostaLC", new[] { "SegmentoID" });
            DropIndex("dbo.PropostaLC", new[] { "TipoClienteID" });
            DropColumn("dbo.PropostaLC", "SegmentoID");
            DropColumn("dbo.PropostaLC", "TipoClienteID");
            DropColumn("dbo.PropostaLC", "TipoProposta");
        }
    }
}
