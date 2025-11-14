using System;

namespace Yara.Domain.Entities
{
    public class PropostaProrrogacaoDetalhe
    {
        public Guid ID { get; set; }
        public Guid PropostaProrrogacaoID { get; set; }
        public virtual PropostaProrrogacao PropostaProrrogacao { get; set; }
        public DateTime? NovoVencimento { get; set; }
        public decimal? ValorPrincipal { get; set; }
        public float? taxaJuros { get; set; }
        public decimal? ValorJuros { get; set; }
        public decimal? ValorAtualizado { get; set; }
        public DateTime? MediaDias { get; set; }
    }
}