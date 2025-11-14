using System;
using Yara.Domain.Entities;

namespace Yara.Data.Tests
{
    public class PermissaoBuilder
    {
        private Guid id = Guid.NewGuid();
        private string nome = "Permissao Teste";
        private string descricao = "Descricao Permissao Teste";
        private DateTime datacriacao = DateTime.Now;
        private DateTime? dataalteracao = null;
        private Boolean ativo = true;

        public Permissao Build()
        {
            var objPermissao = new Permissao();

            objPermissao.Ativo = ativo;
            objPermissao.Nome = nome;
            objPermissao.Descricao = descricao;

            return objPermissao;

        }

        public PermissaoBuilder WithID(Guid ID)
        {
            this.id = ID;
            return this;
        }

        public PermissaoBuilder WithNome(string Nome)
        {
            this.nome = Nome;
            return this;
        }
        public PermissaoBuilder WithDescricao(string descricao)
        {
            this.descricao = descricao;
            return this;
        }

        public PermissaoBuilder WithDataCriacao(DateTime DataCriacao)
        {
            this.datacriacao = DataCriacao;
            return this;
        }

        public PermissaoBuilder WithDataAlteracao(DateTime DataAlteracao)
        {
            this.dataalteracao = DataAlteracao;
            return this;
        }

        public PermissaoBuilder WithAtivo(Boolean Ativo)
        {
            this.ativo = Ativo;
            return this;
        }

        public static implicit operator Permissao(PermissaoBuilder instance)
        {
            return instance.Build();
        }
    }
}