using System;
using Newtonsoft.Json;

namespace Yara.AppService.Dtos
{
    public class SendPendencia
    {
        public Guid ID { get; set; }
        public Guid? RepresentanteID { get; set; }
        public string CodigoSAP { get; set; }
        public string Mensagem { get; set; }
        [JsonIgnore]
        public string EmpresaID { get; set; }

        [JsonIgnore]
        public Guid Usuario { get; set; }
    }
}