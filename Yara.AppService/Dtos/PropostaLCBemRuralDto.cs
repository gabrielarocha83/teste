using Newtonsoft.Json;
using System;

namespace Yara.AppService.Dtos
{
    public class PropostaLCBemRuralDto
    {

        public Guid ID { get; set; }
        public Guid PropostaLCID { get; set; }
        public Guid? GarantiaID { get; set; }
        public string IR { get; set; }
        public Guid? CidadeID { get; set; }
        public decimal? AreaTotalHa { get; set; }
        public decimal? Benfeitorias { get; set; }
        public decimal? Onus { get; set; }
        public string Documento { get; set; }
        // Navigation Properties
        [JsonIgnore]
        public PropostaLCDto PropostaLC { get; set; }
        public virtual PropostaLCGarantiaDto PropostaLCGarantia { get; set; }
        public virtual CidadeDto Cidade { get; set; }

    }
}
