namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDetalheeParcelamento : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PropostaProrrogacaoDetalhe",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        PropostaProrrogacaoID = c.Guid(nullable: false),
                        NovoVencimento = c.DateTime(),
                        ValorPrincipal = c.Decimal(precision: 18, scale: 2),
                        taxaJuros = c.Single(),
                        ValorJuros = c.Decimal(precision: 18, scale: 2),
                        ValorAtualizado = c.Decimal(precision: 18, scale: 2),
                        MediaDias = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.PropostaProrrogacao", t => t.PropostaProrrogacaoID)
                .Index(t => t.PropostaProrrogacaoID);
            
            CreateTable(
                "dbo.PropostaProrrogacaoParcelamento",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        PropostaProrrogacaoID = c.Guid(nullable: false),
                        Parcela = c.Int(nullable: false),
                        Valor = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Ativo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.PropostaProrrogacao", t => t.PropostaProrrogacaoID)
                .Index(t => t.PropostaProrrogacaoID);
            
            AddColumn("dbo.PropostaProrrogacao", "TipoGarantiaID", c => c.Guid());
            CreateIndex("dbo.PropostaProrrogacao", "TipoGarantiaID");
            AddForeignKey("dbo.PropostaProrrogacao", "TipoGarantiaID", "dbo.TipoGarantia", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PropostaProrrogacaoParcelamento", "PropostaProrrogacaoID", "dbo.PropostaProrrogacao");
            DropForeignKey("dbo.PropostaProrrogacaoDetalhe", "PropostaProrrogacaoID", "dbo.PropostaProrrogacao");
            DropForeignKey("dbo.PropostaProrrogacao", "TipoGarantiaID", "dbo.TipoGarantia");
            DropIndex("dbo.PropostaProrrogacaoParcelamento", new[] { "PropostaProrrogacaoID" });
            DropIndex("dbo.PropostaProrrogacaoDetalhe", new[] { "PropostaProrrogacaoID" });
            DropIndex("dbo.PropostaProrrogacao", new[] { "TipoGarantiaID" });
            DropColumn("dbo.PropostaProrrogacao", "TipoGarantiaID");
            DropTable("dbo.PropostaProrrogacaoParcelamento");
            DropTable("dbo.PropostaProrrogacaoDetalhe");
        }
    }
}
