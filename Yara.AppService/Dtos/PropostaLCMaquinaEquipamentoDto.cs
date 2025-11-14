using Newtonsoft.Json;
using System;

namespace Yara.AppService.Dtos
{
    public class PropostaLCMaquinaEquipamentoDto
    {

        // Properties
        public Guid ID { get; set; }
        public Guid PropostaLCID { get; set; }
        public Guid? GarantiaID { get; set; }
        public string Descricao { get; set; }
        public int? Ano { get; set; }
        public decimal? Valor { get; set; }
        public string Documento { get; set; }

        // Navigation Properties
        [JsonIgnore]
        public PropostaLCDto PropostaLC { get; set; }
        public virtual PropostaLCGarantiaDto PropostaLCGarantia { get; set; }


    }
}
