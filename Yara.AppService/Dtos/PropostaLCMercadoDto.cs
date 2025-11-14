using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yara.AppService.Dtos
{
    public class PropostaLCMercadoDto
    {

        public Guid ID { get; set; }
        public Guid PropostaLCID { get; set; }
        public Guid? CulturaID { get; set; }

        [JsonIgnore]
        public PropostaLCDto PropostaLC { get; set; }
        public virtual CulturaDto Cultura { get; set; }

    }
}
