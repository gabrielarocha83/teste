using System;
using Yara.Domain.Entities;

namespace Yara.Data.Tests
{
    public  class LogBuilder
    {
        private int levelID = (int)EnumLogLevel.Info;
        private string navegador = "IE";
        private string descricao = "TESTE INTEGRAÇÃO LOG";
        private string ip = "2321321";
        private string usuario = "JP";
        private Guid _usuarioID = Guid.NewGuid();
        private string pagina = "2321312321";
        private string idioma = "pt-BR";
        private Guid id = Guid.NewGuid();

        public  Log Build()
        {
            var objLog = new Log();
            objLog.IP = ip;
            objLog.Navegador = navegador;
            objLog.Pagina = pagina;
            objLog.Idioma = idioma;
            objLog.ID = Guid.NewGuid();
            objLog.DataCriacao = DateTime.Now;
            objLog.UsuarioID = _usuarioID;
            objLog.LogLevelID = levelID;
            objLog.Descricao = descricao;
            objLog.Usuario = usuario;
            objLog.ID = id;
            return objLog;
        }

        public LogBuilder WithID(Guid ID)
        {
            this.id = ID;
            return this;
        }

        public LogBuilder WithDescricao(string Descricao)
        {
            this.descricao = Descricao;
            return this;
        }

        public LogBuilder WithIP(string IP)
        {
            this.ip = IP;
            return this;
        }

        public LogBuilder WithNavegador(string Navegador)
        {
            this.navegador = Navegador;
            return this;
        }

        public LogBuilder WithLevel(EnumLogLevel logLevel)
        {
            this.levelID = (int) logLevel;
            return this;
        }

        public LogBuilder WithPagina(string Pagina)
        {
            this.pagina = Pagina;
            return this;
        }
        public LogBuilder WithUsuario(string Usuario)
        {
            this.usuario = Usuario;
            return this;
        }

        public LogBuilder WithIdioma(string Idioma)
        {
            this.idioma = Idioma;
            return this;
        }
        public LogBuilder WithUsuarioID(Guid UsuarioID)
        {
            this._usuarioID = UsuarioID;
            return this;
        }


        public static implicit operator Log(LogBuilder instance)
        {
            return instance.Build();
        }
    }
}
