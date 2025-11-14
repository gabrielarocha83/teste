using System;
using Yara.Domain.Entities;

namespace Yara.Data.Tests.Builder
{
    public class ExperienciaBuilder
    {
        private Guid _id = Guid.NewGuid();
        private string _descricao = "Experiencia de teste";
        private bool _ativo = true;
        private DateTime _datacriacao = DateTime.Now;
        private Guid _usuarioCriacao = Guid.Empty;

        public Experiencia Build()
        {
            var exp = new Experiencia()
            {
                ID = _id,
                Descricao = _descricao,
                Ativo = _ativo,
                DataCriacao = _datacriacao,
                UsuarioIDCriacao = Guid.NewGuid()
            };

            return exp;
        }

        public ExperienciaBuilder WithId(Guid id)
        {
            _id = id;
            return this;
        }

        public ExperienciaBuilder WithDescricao(string descricao)
        {
            _descricao = descricao;
            return this;
        }

        public ExperienciaBuilder WithAtivo(bool ativo)
        {
            _ativo = ativo;
            return this;
        }

        public ExperienciaBuilder WithDataCriacao(DateTime dataCriacao)
        {
            _datacriacao = dataCriacao;
            return this;
        }

        public ExperienciaBuilder WithUsuarioCriacao(Guid guid)
        {
            _usuarioCriacao = guid;
            return this;
        }

        public static implicit operator Experiencia(ExperienciaBuilder instance)
        {
            return instance.Build();
        }
    }
}
