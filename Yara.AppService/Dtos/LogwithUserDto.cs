using System;

#pragma warning disable CS0108 // O membro oculta o membro herdado; nova palavra-chave ausente

namespace Yara.AppService.Dtos
{
    public class LogWithUserDto : BaseDto
    {
        public DateTime DataCriacao { get; set; }

        public string Usuario { get; set; }
        public Guid UsuarioID { get; set; }
        public string Login { get; set; }
        public string Descricao { get; set; }
        public string Pagina { get; set; }
        public string Navegador { get; set; }
        public string IP { get; set; }
        public Guid? IDTransacao { get; set; }
        public string Idioma { get; set; }
        public int LogLevelID { get; set; }
        public virtual LogLevelDto LogLevel { get; set; }
    }
}