using System.Collections.Generic;
using Newtonsoft.Json;

namespace Yara.AppService.Dtos
{
    public class CulturaDto : BaseDto
    {
        public string Descricao { get; set; }
        public string UnidadeMedida { get; set; }
        public bool Ativo { get; set; }
        [JsonIgnore]
        public virtual ICollection<PropostaLCDto> PropostaLcs { get; set; }
    }
}
