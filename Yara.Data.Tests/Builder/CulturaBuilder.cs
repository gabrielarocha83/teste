using System;
using Yara.Domain.Entities;

namespace Yara.Data.Tests.Builder
{
    public class CulturaBuilder
    {
        private Guid _id = Guid.NewGuid();
        private string _descricao = "Tipo Cultura de Teste";
        private string _unidademedida = "Tonelada";
        private DateTime _datacriacao = DateTime.Now;
        private bool _ativo = true;
        private Guid _usuarioCriacao = Guid.Empty;

        public Cultura Build()
        {
            var cultura = new Cultura
            {
                ID = _id,
                Descricao = _descricao,
                UnidadeMedida = _unidademedida,
                DataCriacao = _datacriacao,
                Ativo = _ativo
            };
            return cultura;
        }

        public CulturaBuilder WithId(Guid id)
        {
            _id = id;
            return this;
        }

        public CulturaBuilder WithDescricao(string descricao)
        {
            _descricao = descricao;
            return this;
        }

        public CulturaBuilder WithDataCriacao(DateTime dataCriacao)
        {
            _datacriacao = dataCriacao;
            return this;
        }

        public CulturaBuilder WithDataUsuarioCriacao(Guid user)
        {
            _usuarioCriacao = user;
            return this;
        }

        public CulturaBuilder WithAtivo(bool ativo)
        {
            _ativo = ativo;
            return this;
        }

        public static implicit operator Cultura(CulturaBuilder instance)
        {
            return instance.Build();
        }
    }
}
