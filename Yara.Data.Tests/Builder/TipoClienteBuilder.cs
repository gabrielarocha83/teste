using System;
using Yara.Domain.Entities;

namespace Yara.Data.Tests
{
    public class TipoClienteBuilder
    {
        private Guid id = Guid.NewGuid();
        private string nome = "";
        private string descricao = "";
        private DateTime datacriacao = DateTime.Now;
        private DateTime? dataalteracao = null;
        private Boolean ativo = true;

        public TipoCliente Build()
        {
            var objGrupo = new TipoCliente();

            objGrupo.Ativo = ativo;
            objGrupo.ID = id;
            objGrupo.Nome = nome;
            objGrupo.DataCriacao = datacriacao;
            objGrupo.DataAlteracao = dataalteracao;

            return objGrupo;

        }

        public TipoClienteBuilder WithID(Guid ID)
        {
            this.id = ID;
            return this;
        }

        public TipoClienteBuilder WithNome(string Nome)
        {
            this.nome = Nome;
            return this;
        }

        public TipoClienteBuilder WithDescricao(string Descricao)
        {
            this.descricao = Descricao;
            return this;
        }

        public TipoClienteBuilder WithDataCriacao(DateTime DataCriacao)
        {
            this.datacriacao = DataCriacao;
            return this;
        }

        public TipoClienteBuilder WithDataAlteracao(DateTime DataAlteracao)
        {
            this.dataalteracao = DataAlteracao;
            return this;
        }

        public TipoClienteBuilder WithAtivo(Boolean Ativo)
        {
            this.ativo = Ativo;
            return this;
        }

        public static implicit operator TipoCliente(TipoClienteBuilder instance)
        {
            return instance.Build();
        }
    }
}