namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Titulos1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Titulo",
                c => new
                    {
                        NumeroDocumento = c.String(nullable: false, maxLength: 10, fixedLength: true, unicode: false),
                        Linha = c.String(nullable: false, maxLength: 3, fixedLength: true, unicode: false),
                        AnoExercicio = c.String(nullable: false, maxLength: 4, fixedLength: true, unicode: false),
                        Empresa = c.String(nullable: false, maxLength: 4, fixedLength: true, unicode: false),
                        TipoDocumento = c.String(maxLength: 2, unicode: false),
                        DataEmissaoDocumento = c.DateTime(nullable: false),
                        CodigoRazao = c.String(maxLength: 1, fixedLength: true, unicode: false),
                        CodigoCliente = c.String(nullable: false, maxLength: 10, fixedLength: true, unicode: false),
                        OrdemVendaNumero = c.String(maxLength: 10, unicode: false),
                        OrdemVendaItem = c.Int(nullable: false),
                        NotaFiscal = c.String(maxLength: 20, unicode: false),
                        ValorInterno = c.Decimal(nullable: false, precision: 13, scale: 2),
                        ValorDocumento = c.Decimal(nullable: false, precision: 13, scale: 2),
                        DataVencimento = c.DateTime(nullable: false),
                        DataOriginal = c.DateTime(),
                        TextoDocumento = c.String(maxLength: 50, unicode: false),
                        InstrumentoPagamento = c.String(maxLength: 1, fixedLength: true, unicode: false),
                        NumeroDocumentoCompensacao = c.String(maxLength: 10, fixedLength: true, unicode: false),
                        MoedaInterna = c.String(maxLength: 5, unicode: false),
                        MoedaDocumento = c.String(maxLength: 5, unicode: false),
                        CreditoDebito = c.String(nullable: false, maxLength: 1, fixedLength: true, unicode: false),
                        CobrancaAutomatica = c.String(nullable: false, maxLength: 5, unicode: false),
                        Aberto = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.NumeroDocumento, t.Linha, t.AnoExercicio });
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Titulo");
        }
    }
}
