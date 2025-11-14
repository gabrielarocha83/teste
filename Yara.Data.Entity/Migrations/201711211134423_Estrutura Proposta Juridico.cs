namespace Yara.Data.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EstruturaPropostaJuridico : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PropostaJuridico",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        NumeroInternoProposta = c.Int(nullable: false),
                        ComentarioHistorico = c.String(maxLength: 120, unicode: false),
                        ParecerVisita = c.String(maxLength: 120, unicode: false),
                        ValorEnvio = c.Decimal(precision: 18, scale: 2),
                        ValorDebito = c.Decimal(precision: 18, scale: 2),
                        PercentualPdd = c.Decimal(precision: 18, scale: 2),
                        ProrrogacaoAnterior = c.Boolean(nullable: false),
                        Aceite = c.Boolean(nullable: false),
                        Protesto = c.Boolean(nullable: false),
                        BuscaBens = c.Boolean(nullable: false),
                        Pedidos = c.Boolean(nullable: false),
                        NotaFiscal = c.Boolean(nullable: false),
                        Comprovante = c.Boolean(nullable: false),
                        ParecerCobranca = c.String(maxLength: 120, unicode: false),
                        PropostaJuridicoStatus = c.Int(nullable: false),
                        ResponsavelID = c.Guid(nullable: false),
                        UsuarioIDCriacao = c.Guid(nullable: false),
                        UsuarioIDAlteracao = c.Guid(),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.PropostaJuridicoHistoricoPagamento",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        PropostaJuridicoID = c.Guid(nullable: false),
                        DataPagamento = c.DateTime(nullable: false),
                        OrdemVendaNumero = c.String(maxLength: 120, unicode: false),
                        NotaFiscal = c.String(maxLength: 120, unicode: false),
                        ValorDocumento = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.PropostaJuridico", t => t.PropostaJuridicoID)
                .Index(t => t.PropostaJuridicoID);
            
            CreateTable(
                "dbo.PropostaJuridicoTitulo",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        PropostaJuridicoID = c.Guid(nullable: false),
                        NumeroDocumento = c.String(maxLength: 120, unicode: false),
                        Linha = c.String(maxLength: 120, unicode: false),
                        AnoExercicio = c.String(maxLength: 120, unicode: false),
                        Empresa = c.String(maxLength: 120, unicode: false),
                        TipoDocumento = c.String(maxLength: 120, unicode: false),
                        DataEmissaoDocumento = c.DateTime(nullable: false),
                        CodigoRazao = c.String(maxLength: 120, unicode: false),
                        CodigoCliente = c.String(maxLength: 120, unicode: false),
                        OrdemVendaNumero = c.String(maxLength: 120, unicode: false),
                        OrdemVendaItem = c.Int(nullable: false),
                        NotaFiscal = c.String(maxLength: 120, unicode: false),
                        ValorInterno = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ValorDocumento = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DataVencimento = c.DateTime(nullable: false),
                        DataOriginal = c.DateTime(),
                        TextoDocumento = c.String(maxLength: 120, unicode: false),
                        InstrumentoPagamento = c.String(maxLength: 120, unicode: false),
                        NumeroDocumentoCompensacao = c.String(maxLength: 120, unicode: false),
                        MoedaInterna = c.String(maxLength: 120, unicode: false),
                        MoedaDocumento = c.String(maxLength: 120, unicode: false),
                        CreditoDebito = c.String(maxLength: 120, unicode: false),
                        CobrancaAutomatica = c.String(maxLength: 120, unicode: false),
                        Aberto = c.Boolean(nullable: false),
                        TaxaJuros = c.Decimal(precision: 18, scale: 2),
                        DataDuplicata = c.DateTime(),
                        DataTriplicata = c.DateTime(),
                        DataPefinInclusao = c.DateTime(),
                        DataPefinExclusao = c.DateTime(),
                        DataProtesto = c.DateTime(),
                        DataAceite = c.DateTime(),
                        DataPrevisaoPagamento = c.DateTime(),
                        DataPR = c.DateTime(),
                        DataREPR = c.DateTime(),
                        DataProtestoRealizado = c.DateTime(),
                        StatusCobrancaID = c.Guid(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.PropostaJuridico", t => t.PropostaJuridicoID)
                .Index(t => t.PropostaJuridicoID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PropostaJuridicoTitulo", "PropostaJuridicoID", "dbo.PropostaJuridico");
            DropForeignKey("dbo.PropostaJuridicoHistoricoPagamento", "PropostaJuridicoID", "dbo.PropostaJuridico");
            DropIndex("dbo.PropostaJuridicoTitulo", new[] { "PropostaJuridicoID" });
            DropIndex("dbo.PropostaJuridicoHistoricoPagamento", new[] { "PropostaJuridicoID" });
            DropTable("dbo.PropostaJuridicoTitulo");
            DropTable("dbo.PropostaJuridicoHistoricoPagamento");
            DropTable("dbo.PropostaJuridico");
        }
    }
}
