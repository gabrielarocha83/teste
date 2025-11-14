using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Yara.AppService.Dtos
{
    public class MovimentacaoEstruturaComercialDto
    {
        public string CodSap { get; set; }
        public string RepresentanteID { get; set; }
        public string EmpresaId { get; set; }
        public IEnumerable<ContaClienteDto> ContaClientes { get; set; }

        [JsonIgnore]
        public Guid UsuarioIDCriacao { get; set; }
        [JsonIgnore]
        public Guid? UsuarioIDAlteracao { get; set; }
        [JsonIgnore]
        public DateTime DataCriacao { get; set; }
        [JsonIgnore]
        public DateTime? DataAlteracao { get; set; }
    }
}