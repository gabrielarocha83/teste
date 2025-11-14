using System;

namespace Yara.AppService.Dtos
{
    public class BuscaCockpitPropostaRenovacaoVigenciaLCDto
    {
        // Base
        public Guid ID { get; set; }
        public DateTime DataCriacao { get; set; }

        // PropostaRenovacaoVigenciaLC
        public int NumeroInternoProposta { get; set; }

        public string Status { get; set; }
        public decimal Montante { get; set; }
        public DateTime DataNovaVigencia { get; set; }
        public string Responsavel { get; set; }
        public DateTime? VigenciaLc { get; set; }
        public decimal LcAtual { get; set; }

        public string CodigoProposta
        {
            get
            {
                return string.Format("RV{0:00000}/{1:yyyy}", this.NumeroInternoProposta, this.DataCriacao);
            }
        }
    }
}
