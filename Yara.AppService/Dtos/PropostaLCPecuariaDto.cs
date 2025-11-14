using System;
using Newtonsoft.Json;

namespace Yara.AppService.Dtos
{
    public class PropostaLCPecuariaDto
    {
        public Guid ID { get; set; }
        public Guid? PropostaLCID { get; set; }
        public Guid? TipoPecuariaID { get; set; }
        public string Documento { get; set; }
        public int? AnoPecuaria { get; set; }
        public decimal? Quantidade { get; set; }
        public decimal? Preco { get; set; }
        public decimal? Despesa { get; set; }

        [JsonIgnore]
        public PropostaLCDto PropostaLC { get; set; }
        public virtual TipoPecuariaDto TipoPecuaria { get; set; }
    }
}