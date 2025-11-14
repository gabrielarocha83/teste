using System;

namespace Yara.Domain.Entities
{
    public class PropostaLCGrupoEconomico
    {

        // Properties
        public Guid PropostaLCID { get; set; }
        public virtual PropostaLC PropostaLC { get; set; }

        public string Documento { get; set; }
        public decimal? PotencialPatrimonial { get; set; }
        public decimal? PotencialCredito { get; set; }
        public decimal? PotencialReceita { get; set; }
        public decimal? LimiteSugerido { get; set; }
        public DateTime? VigenciaSugerida { get; set; }
        public DateTime? VigenciaFimSugerida { get; set; }
        public Guid? DemonstrativoID { get; set; }
        public string Rating { get; set; }
        public PropostaLCDemonstrativo Demonstrativo { get; set; }


    }
}


