using System;

namespace Yara.AppService.Dtos
{
    public class PropostaJuridicoTituloDto
    {
        public Guid ID { get; set; }
        public Guid PropostaJuridicoID { get; set; }
        public string NumeroDocumento { get; set; }
        public string Linha { get; set; }
        public string AnoExercicio { get; set; }
        public string Empresa { get; set; }
        public string TipoDocumento { get; set; }
        public DateTime DataEmissaoDocumento { get; set; }
        public string CodigoRazao { get; set; }
        public string CodigoCliente { get; set; }
        public string OrdemVendaNumero { get; set; }
        public int OrdemVendaItem { get; set; }
        public string NotaFiscal { get; set; }
        public decimal ValorInterno { get; set; }
        public decimal ValorDocumento { get; set; }
        public DateTime DataVencimento { get; set; }
        public DateTime? DataOriginal { get; set; }
        public string TextoDocumento { get; set; }
        public string InstrumentoPagamento { get; set; }
        public string NumeroDocumentoCompensacao { get; set; }
        public string MoedaInterna { get; set; }
        public string MoedaDocumento { get; set; }
        public string CreditoDebito { get; set; }
        public string CobrancaAutomatica { get; set; }
        public bool Aberto { get; set; }
        public decimal? TaxaJuros { get; set; }
        public DateTime? DataDuplicata { get; set; }
        public DateTime? DataTriplicata { get; set; }
        public DateTime? DataPefinInclusao { get; set; }
        public DateTime? DataPefinExclusao { get; set; }
        public DateTime? DataProtesto { get; set; }
        public DateTime? DataAceite { get; set; }
        public DateTime? DataPrevisaoPagamento { get; set; }
        public DateTime? DataPR { get; set; }
        public DateTime? DataREPR { get; set; }
        public DateTime? DataProtestoRealizado { get; set; }
        public Guid? StatusCobrancaID { get; set; }

        public PropostaJuridicoDto PropostaJuridico { get; set; }
    }
}
