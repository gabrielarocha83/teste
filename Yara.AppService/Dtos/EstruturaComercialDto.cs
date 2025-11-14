using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Yara.AppService.Dtos
{
    public class EstruturaComercialDto 
    {
       
        public string CodigoSap { get; set; }
        [JsonIgnore]
        public Guid UsuarioIDCriacao { get; set; }
        [JsonIgnore]
        public Guid? UsuarioIDAlteracao { get; set; }
        [JsonIgnore]
        public DateTime DataCriacao { get; set; }
        [JsonIgnore]
        public DateTime? DataAlteracao { get; set; }
        public string Nome { get; set; }
        public virtual EstruturaComercialDto Superior { get; set; }

        public bool Ativo { get; set; }
        public string EstruturaComercialPapelID { get; set; }

        [JsonIgnore]
        public virtual EstruturaComercialPapelDto EstruturaComercialPapel { get; set; }

        [JsonIgnore]
        public virtual ICollection<ContaClienteEstruturaComercialDto> ContaClienteEstruturaComercial { get; set; }

        [JsonIgnore]
        public virtual ICollection<UsuarioDto> Usuarios { get; set; }

        public EstruturaComercialDto()
        {
            ContaClienteEstruturaComercial = new List<ContaClienteEstruturaComercialDto>();
            Usuarios = new List<UsuarioDto>();
        }
    }
}