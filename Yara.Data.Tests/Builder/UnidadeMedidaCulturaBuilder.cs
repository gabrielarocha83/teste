using System;
using Yara.Domain.Entities;

namespace Yara.Data.Tests.Builder
{
    public class UnidadeMedidaCulturaBuilder
    {
        private Guid _id = Guid.NewGuid();
        private string _nome = "";
        private string _sigla = "";
        private DateTime _datacriacao = DateTime.Now;
        private Guid _usuarioidcriacao = Guid.Empty;
        private Guid? _usuarioidalteracao = Guid.Empty;
        private bool _ativo = true;

        public UnidadeMedidaCultura Build()
        {
            var UnidadeMedidaCultura = new UnidadeMedidaCultura
            {
                ID = _id,
                Nome = _nome,
                Sigla = _sigla,
                DataCriacao = _datacriacao,
                UsuarioIDCriacao = _usuarioidcriacao,
                UsuarioIDAlteracao = _usuarioidalteracao,
                Ativo = _ativo
            };
            return UnidadeMedidaCultura;
        }

        public UnidadeMedidaCulturaBuilder WithId(Guid id)
        {
            _id = id;
            return this;
        }

        public UnidadeMedidaCulturaBuilder WithNome(string nome)
        {
            _nome = nome;
            return this;
        }

        public UnidadeMedidaCulturaBuilder WithSigla(string sigla)
        {
            _sigla = sigla;
            return this;
        }
        public UnidadeMedidaCulturaBuilder WithUsuarioIDCriacao(Guid usuarioidcriacao)
        {
            _usuarioidcriacao = usuarioidcriacao;
            return this;
        }

        public UnidadeMedidaCulturaBuilder WithUsuarioIDAlteracao(Guid? usuarioidalteracao)
        {
            _usuarioidalteracao = usuarioidalteracao;
            return this;
        }


        public UnidadeMedidaCulturaBuilder WithDataCriacao(DateTime dataCriacao)
        {
            _datacriacao = dataCriacao;
            return this;
        }

        public UnidadeMedidaCulturaBuilder WithAtivo(bool ativo)
        {
            _ativo = ativo;
            return this;
        }

        public static implicit operator UnidadeMedidaCultura(UnidadeMedidaCulturaBuilder instance)
        {
            return instance.Build();
        }
    }
}
