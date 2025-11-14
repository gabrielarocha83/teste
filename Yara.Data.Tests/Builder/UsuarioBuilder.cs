using System;
using Yara.Domain.Entities;

namespace Yara.Data.Tests.Builder
{
    public class UsuarioBuilder
    {
        private Guid _id = Guid.NewGuid();
        private string _nome = "Usuario de Teste";
        private DateTime _datacriacao = DateTime.Now;
        private DateTime? _dataalteracao = null;
        private bool _ativo = true;
        private string _email = "usuarioteste@teste.com.br";
        private string _login = "usuarioteste";
        private TipoAcesso _tipoAcesso = TipoAcesso.AD;
        public Usuario Build()
        {
            var usuario = new Usuario
            {
                ID = _id,
                Nome = _nome,
                DataCriacao = _datacriacao,
                DataAlteracao = _dataalteracao,
                Email = _email,
                Login = _login,
                Ativo = _ativo,
                TipoAcesso = _tipoAcesso
            };
            return usuario;
        }

        public UsuarioBuilder WithId(Guid id)
        {
            _id = id;
            return this;
        }

        public UsuarioBuilder WithNome(string nome)
        {
            _nome = nome;
            return this;
        }

        public UsuarioBuilder WithEmail(string email)
        {
            _email = email;
            return this;
        }

        public UsuarioBuilder WithLogin(string login)
        {
            _login = login;
            return this;
        }
        public UsuarioBuilder WithTipoAcesso(TipoAcesso tipoacesso)
        {
            _tipoAcesso = tipoacesso;
            return this;
        }

        public UsuarioBuilder WithDataCriacao(DateTime dataCriacao)
        {
            _datacriacao = dataCriacao;
            return this;
        }

        public UsuarioBuilder WithDataAlteracao(DateTime dataAltercao)
        {
            _dataalteracao = dataAltercao;
            return this;
        }

        public UsuarioBuilder WithAtivo(bool ativo)
        {
            _ativo = ativo;
            return this;
        }

        public static implicit operator Usuario(UsuarioBuilder instance)
        {
            return instance.Build();
        }
    }
}
