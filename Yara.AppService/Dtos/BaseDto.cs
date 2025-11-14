using Newtonsoft.Json;
using System;

namespace Yara.AppService.Dtos
{
    public class BaseDto
    {
        public Guid ID { get; set; }
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