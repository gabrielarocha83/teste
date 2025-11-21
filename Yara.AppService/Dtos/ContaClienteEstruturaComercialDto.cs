using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Yara.AppService.Dtos
{
    public class ContaClienteEstruturaComercialDto
    {

        public Guid ContaClienteId { get; set; }
        public string EstruturaComercialId { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public Guid? UsuarioIDAlteracao { get; set; }
        public string EmpresasId { get; set; }
        public bool Ativo { get; set; }

        [JsonIgnore]
        public ContaClienteDto ContaCliente { get; set; }

        public virtual EstruturaComercialDto EstruturaComercial { get; set; }

        [JsonIgnore]
        public virtual EmpresasDto Empresas { get; set; }
    }
}
