using System;
using Yara.Domain.Entities;

namespace Yara.Data.Tests.Builder
{
    public class TipoGarantiaBuilder
    {
        private Guid _id = Guid.NewGuid();
        private string _nome = "Tipo Garantia de Teste";
        private DateTime _datacriacao = DateTime.Now;
        private bool _ativo = true;
        private Guid _usuarioidcriacao = Guid.NewGuid();
        private Guid? _usuarioidalteracao = Guid.NewGuid();
        public TipoGarantia Build()
        {
            var cultura = new TipoGarantia()
            {
                ID = _id,
                Nome = _nome,
                DataCriacao = _datacriacao,
                Ativo = _ativo,
                UsuarioIDCriacao = _usuarioidcriacao,
                DataAlteracao = DateTime.Now,
                UsuarioIDAlteracao = _usuarioidalteracao
            };
            return cultura;
        }

        public TipoGarantiaBuilder WithId(Guid id)
        {
            _id = id;
            return this;
        }

        public TipoGarantiaBuilder WithNome(string nome)
        {
            _nome = nome;
            return this;
        }

        public TipoGarantiaBuilder WithDataCriacao(DateTime dataCriacao)
        {
            _datacriacao = dataCriacao;
            return this;
        }
        public TipoGarantiaBuilder WithUsuarioIDCriacao(Guid usuarioIDCriacao)
        {
            _usuarioidcriacao = usuarioIDCriacao;
            return this;
        }

        public TipoGarantiaBuilder WithUsuarioIDAlteracao(Guid? usuarioIDAlteracao)
        {
            _usuarioidalteracao = usuarioIDAlteracao;
            return this;
        }
        public TipoGarantiaBuilder WithAtivo(bool ativo)
        {
            _ativo = ativo;
            return this;
        }

        public static implicit operator TipoGarantia(TipoGarantiaBuilder instance)
        {
            return instance.Build();
        }
    }
}
