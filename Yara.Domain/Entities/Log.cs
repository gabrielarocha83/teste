using System;

namespace Yara.Domain.Entities
{
    public class Log : Base
    {
        public string Usuario { get; set; }
        public Guid UsuarioID { get; set; }
        public string Descricao { get; set; }
        public string Pagina { get; set; }
        public string Navegador { get; set; }
        public string IP { get; set; }
        public Guid? IDTransacao { get; set; }
        public string Idioma { get; set; }
        public int LogLevelID { get; set; }
        public virtual LogLevel LogLevel { get; set; }
    }
}
