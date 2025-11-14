using System;
using Newtonsoft.Json;

namespace Yara.AppService.Dtos
{
    public class ContaClienteEstruturaComercialDto
    {
        public Guid ContaClienteId { get; set; }
        public string EstruturaComercialId { get; set; }
        public DateTime DataCriacao { get; set; }
        public string EmpresasId { get; set; }

        [JsonIgnore]
        public ContaClienteDto ContaCliente { get; set; }

        public virtual EstruturaComercialDto EstruturaComercial { get; set; }

        [JsonIgnore]
        public virtual EmpresasDto Empresas { get; set; }
    }
}
