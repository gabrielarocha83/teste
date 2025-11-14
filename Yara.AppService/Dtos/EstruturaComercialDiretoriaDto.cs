using System;
using System.Collections.Generic;
using Newtonsoft.Json;

//using System.Collections.Generic;
// ReSharper disable InconsistentNaming

namespace Yara.AppService.Dtos
{
    public class EstruturaComercialDiretoriaDto
    {
       
        public string CodigoSap { get; set; }
       
        public string Nome { get; set; }

        public List<EstruturaComercialDto> Gerentes { get; set; }

        [JsonIgnore]
        public Guid UsuarioIDCriacao { get; set; }
        [JsonIgnore]
        public Guid? UsuarioIDAlteracao { get; set; }
        [JsonIgnore]
        public DateTime DataCriacao { get; set; }
        [JsonIgnore]
        public DateTime? DataAlteracao { get; set; }

        public bool Ativo { get; set; }
        // [JsonIgnore]
        //public virtual ICollection<ContaClienteDto> ContaClientes { get; set; }
    }
}