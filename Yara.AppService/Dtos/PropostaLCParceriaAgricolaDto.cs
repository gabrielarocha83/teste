using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yara.AppService.Dtos
{
    public class PropostaLCParceriaAgricolaDto
    {
        // Properties
        public Guid ID { get; set; }
        public Guid PropostaLCID { get; set; }
        public string Documento { get; set; }
        public string InscricaoEstadual { get; set; }
        public string Nome { get; set; }
        public Guid? SolicitanteSerasaID { get; set; }
        public bool RestricaoSerasa { get; set; }
        [JsonIgnore]
        public PropostaLCDto PropostaLC { get; set; }
    }
}
