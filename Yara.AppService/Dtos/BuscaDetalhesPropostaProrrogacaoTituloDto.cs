using System;

namespace Yara.AppService.Dtos
{
    public class BuscaDetalhesPropostaProrrogacaoTituloDto
    {
        public DateTime? DataVencimento { get; set; }
        public decimal? ValorPrincipal { get; set; }
        public decimal? TaxaJuros { get; set; }
        public decimal? ValorJuros { get; set; }
        public decimal? ValorAtualizado { get; set; }
        public int? Media { get; set; }
    }
}
