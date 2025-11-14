using System;

namespace Yara.AppService.Dtos
{
    public class PropostaLCGrupoEconomicoDto
    {

        // Properties
        public Guid PropostaLCID { get; set; }

        public virtual PropostaLCDto PropostaLC { get; set; }
        public string Documento { get; set; }
        public decimal? PotencialPatrimonial { get; set; }
        public decimal? PotencialCredito { get; set; }
        public decimal? PotencialReceita { get; set; }
        public decimal? LimiteSugerido { get; set; }
        public DateTime? VigenciaSugerida { get; set; }
        public DateTime? VigenciaFimSugerida { get; set; }
        public Guid? DemonstrativoID { get; set; }
        public string Rating { get; set; }
        public PropostaLCDemonstrativoDto Demonstrativo { get; set; }


    }
}
