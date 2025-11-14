using System;
using Yara.Domain.Entities;

namespace Yara.Data.Tests.Builder
{
    public class ReceitaBuilder
    {
        private Guid _id = Guid.NewGuid();
        private string _descricao = "Descrição Receita de Teste";
        private DateTime _datacriacao = DateTime.Now;
        private bool _ativo = true;

        public Receita Build()
        {
            var receita = new Receita
            {
                ID = _id,
                Descricao = _descricao,
                DataCriacao = _datacriacao,
                Ativo = _ativo
            };
            return receita;
        }

        public ReceitaBuilder WithId(Guid id)
        {
            _id = id;
            return this;
        }

        public ReceitaBuilder WithDescricao(string descricao)
        {
            _descricao = descricao;
            return this;
        }

        public ReceitaBuilder WithDataCriacao(DateTime dataCriacao)
        {
            _datacriacao = dataCriacao;
            return this;
        }


        public ReceitaBuilder WithAtivo(bool ativo)
        {
            _ativo = ativo;
            return this;
        }

        public static implicit operator Receita(ReceitaBuilder instance)
        {
            return instance.Build();
        }
    }
}
