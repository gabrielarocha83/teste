using Newtonsoft.Json;
using System;

namespace Yara.AppService.Dtos
{
    public class ContaClienteComentarioDto 
    {
        public Guid ID { get; set; }
        public Guid ContaClienteID { get; set; }
        public Guid UsuarioID { get; set; }
        public string Descricao { get; set; }
        public bool Ativo { get; set; }
        [JsonIgnore]
        public virtual ContaClienteDto ContaCliente { get; set; }
        [JsonIgnore]
        public Guid UsuarioIDCriacao { get; set; }
        [JsonIgnore]
        public Guid? UsuarioIDAlteracao { get; set; }
        public DateTime DataCriacao { get; set; }
        [JsonIgnore]
        public DateTime? DataAlteracao { get; set; }
        public virtual UsuarioDto Usuario { get; set; }
    }
}
