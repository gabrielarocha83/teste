using System;

namespace Yara.Domain.Entities.Procedures
{
    public class BuscaCockpitPropostaRenovacaoVigenciaLC
    {
        public Guid ID { get; set; }
        public DateTime DataCriacao { get; set; }
        public int NumeroInternoProposta { get; set; }
        public string Status { get; set; }
        public decimal Montante { get; set; }
        public DateTime DataNovaVigencia { get; set; }
        public string Responsavel { get; set; }
        public DateTime? VigenciaLc { get; set; }
        public decimal LcAtual { get; set; }
    }
}
