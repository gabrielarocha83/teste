using System;

namespace Yara.Domain.Entities
{
    public class LogwithUser : Base
    {
        //public Log(string ip, string navegador, string pagina, string idioma)
        //{
        //    this.IP = ip;
        //    this.Navegador = navegador;
        //    this.Pagina = pagina;
        //    this.Idioma = idioma;
        //}
        public virtual LogLevel LogLevel { get; set; }
        public int LogLevelID { get; set; }
        public string Usuario { get; set; }
        public Guid UsuarioID { get; set; }
        public string Login { get; set; }
        public string Descricao { get; set; }
        public string Pagina { get; set; }
        public string Navegador { get; set; }
        public string IP { get; set; }
        public Guid? IDTransacao { get; set; }
        public string Idioma { get; set; }
    }
}
