using Newtonsoft.Json;
using System;

#pragma warning disable CS0108 // O membro oculta o membro herdado; nova palavra-chave ausente

namespace Yara.AppService.Dtos
{
    public class LogContaClienteDto : BaseDto
    {
        public DateTime DataCriacao { get; set; }

        public string Usuario { get; set; }
        public Guid UsuarioID { get; set; }
        public string Login { get; set; }
        public string Descricao { get; set; }
        [JsonIgnore]
        public string Pagina { get; set; }
        [JsonIgnore]
        public string Navegador { get; set; }
        [JsonIgnore]
        public string IP { get; set; }
        [JsonIgnore]
        public Guid? IDTransacao { get; set; }
        [JsonIgnore]
        public string Idioma { get; set; }
        [JsonIgnore]
        public int LogLevelID { get; set; }
        [JsonIgnore]
        public virtual LogLevelDto LogLevel { get; set; }
    }
}
