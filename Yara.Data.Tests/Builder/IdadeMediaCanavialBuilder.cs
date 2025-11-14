using System;
using Yara.Domain.Entities;

namespace Yara.Data.Tests.Builder
{
    public class IdadeMediaCanavialBuilder
    {
        private Guid _id = Guid.NewGuid();
        private string _nome = "";
        private DateTime _datacriacao = DateTime.Now;
        private bool _ativo = true;
        private Guid _usuarioidcriacao = Guid.NewGuid();
        private Guid? _usuarioidalteracao = Guid.NewGuid();
        public IdadeMediaCanavial Build()
        {
            var idadeMediaCanavial = new IdadeMediaCanavial()
            {
                ID = _id, 
                Nome = _nome,
                DataCriacao = _datacriacao,
                Ativo = _ativo,
                UsuarioIDCriacao = _usuarioidcriacao,
                DataAlteracao = DateTime.Now,
                UsuarioIDAlteracao = _usuarioidalteracao
            };
            return idadeMediaCanavial;
        }

        public IdadeMediaCanavialBuilder WithId(Guid id)
        {
            _id = id;
            return this;
        }

        public IdadeMediaCanavialBuilder WithNome(string nome)
        {
            _nome = nome;
            return this;
        }

        public IdadeMediaCanavialBuilder WithDataCriacao(DateTime dataCriacao)
        {
            _datacriacao = dataCriacao;
            return this;
        }
        public IdadeMediaCanavialBuilder WithUsuarioIDCriacao(Guid usuarioIDCriacao)
        {
            _usuarioidcriacao = usuarioIDCriacao;
            return this;
        }

        public IdadeMediaCanavialBuilder WithUsuarioIDAlteracao(Guid? usuarioIDAlteracao)
        {
            _usuarioidalteracao = usuarioIDAlteracao;
            return this;
        }
        public IdadeMediaCanavialBuilder WithAtivo(bool ativo)
        {
            _ativo = ativo;
            return this;
        }

        public static implicit operator IdadeMediaCanavial(IdadeMediaCanavialBuilder instance)
        {
            return instance.Build();
        }
    }
}
