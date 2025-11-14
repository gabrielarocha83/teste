using System;

namespace Yara.Domain.Entities.Procedures
{
    public class BuscaDetalhesPropostaProrrogacaoTitulo
    {
        public DateTime? DataVencimento { get; set; }
        public decimal? ValorPrincipal { get; set; }
        public decimal? TaxaJuros { get; set; }
        public decimal? ValorJuros { get; set; }
        public decimal? ValorAtualizado { get; set; }
        public int? Media { get; set; }
    }
}
