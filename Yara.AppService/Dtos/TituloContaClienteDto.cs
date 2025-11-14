using System;
using System.Collections.Generic;

namespace Yara.AppService.Dtos
{
    public class TituloContaClienteDto
    {


        public string NumeroDocumento { get; set; }
        public string Linha { get; set; }
        public string AnoExercicio { get; set; }
        public string Empresa { get; set; }
        public string NomeCTC { get; set; }
        public string NotaFiscal { get; set; }
        public string OrdemVendaNumero { get; set; }
        public DateTime? DataEmissaoDocumento { get; set; }
        public DateTime? VencimentoOriginal { get; set; }
        public DateTime? DataVencimento { get; set; }
        public string CondPagto { get; set; }
        public decimal? ValorInterno { get; set; }
        public decimal? TaxaCambio { get; set; }
        public decimal? TaxaCambioFat { get; set; }
        public decimal? TaxaJuros { get; set; }
        public decimal? ValorJuros { get; set; }
        public decimal? ValorAtual { get; set; }
        public DateTime? PrevisaoPagamento { get; set; }
        public string UltimoComentario { get; set; }
        public Guid StatusCobrancaID { get; set; }
        public string DescricaoStatusCobranca { get; set; }
        public string FaixaVencimento { get; set; }
        public int? Dias { get; set; }
        public string TipoPR { get; set; }
        public string TipoVencimento { get; set; }
        public DateTime? DataDuplicata { get; set; }
        public DateTime? DataTriplicata { get; set; }
        public DateTime? DataAceite { get; set; }
        public DateTime? DataPefinInclusao { get; set; }
        public DateTime? DataPefinExclusao { get; set; }
        public DateTime? DataProtesto { get; set; }
        public DateTime? DataProtestoRealizado { get; set; }
        public decimal? QtdEntregue { get; set; }
        public decimal? QtdPendente { get; set; }
        public bool NaoCobranca { get; set; }
        public Guid ContaClienteID { get; set; }
        public string PropostaStatus { get; set; }

    }

    public class TituloContaClienteTotalizadoDto
    {

        public IEnumerable<TituloContaClienteDto> Titulos { get; set; }
        public int QtdAVencer { get; set; }
        public int QtdVencido { get; set; }
        public decimal TotalAVencer { get; set; }
        public decimal TotalVencido { get; set; }

    }
}
