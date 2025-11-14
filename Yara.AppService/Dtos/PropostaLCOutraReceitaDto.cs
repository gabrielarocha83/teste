using System;
using Newtonsoft.Json;

namespace Yara.AppService.Dtos
{
    public class PropostaLCOutraReceitaDto
    {

        public Guid ID { get; set; }
        public Guid? PropostaLCID { get; set; }
        public Guid? ReceitaID { get; set; }
        public string Documento { get; set; }
        public int? AnoOutrasReceitas { get; set; }
        public decimal? ReceitaPrevista { get; set; }
        
        [JsonIgnore]
        public PropostaLCDto PropostaLC { get; set; }
        public virtual ReceitaDto Receita { get; set; }
    }
}