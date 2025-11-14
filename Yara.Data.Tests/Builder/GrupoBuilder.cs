using System;
using Yara.Domain.Entities;

namespace Yara.Data.Tests
{
    public class GrupoBuilder
    {
        private Guid id = Guid.NewGuid();
        private string nome = "Grupo Teste";
        private DateTime datacriacao = DateTime.Now;
        private DateTime? dataalteracao = null;
        private Boolean ativo = true;

        public Grupo Build()
        {
            var objGrupo = new Grupo();

            objGrupo.Ativo = ativo;
            objGrupo.ID = id;
            objGrupo.Nome = nome;
            objGrupo.DataCriacao = datacriacao;
            objGrupo.DataAlteracao = dataalteracao;

            return objGrupo;

        }

        public GrupoBuilder WithID(Guid ID)
        {
            this.id = ID;
            return this;
        }

        public GrupoBuilder WithNome(string Nome)
        {
            this.nome = Nome;
            return this;
        }

        public GrupoBuilder WithDataCriacao(DateTime DataCriacao)
        {
            this.datacriacao = DataCriacao;
            return this;
        }

        public GrupoBuilder WithDataAlteracao(DateTime DataAlteracao)
        {
            this.dataalteracao = DataAlteracao;
            return this;
        }

        public GrupoBuilder WithAtivo(Boolean Ativo)
        {
            this.ativo = Ativo;
            return this;
        }

        public static implicit operator Grupo(GrupoBuilder instance)
        {
            return instance.Build();
        }
    }
}