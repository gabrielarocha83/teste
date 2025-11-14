using System;
using Yara.Domain.Entities;

namespace Yara.Data.Tests.Builder
{
    public class AnexoBuilder
    {
        private Guid _id = Guid.NewGuid();
        private string _descricao = "Registro Geral";
        private string _descricaoAbreviado = "RG";
        private DateTime _datacriacao = DateTime.Now;
        private bool _obrigatorio = true;
        private int _formulario = 1;
        private string _descricaoFormulario = "Formulario de Rascunho";
        private bool _ativo = true;

        public Anexo Build()
        {
            var anexo = new Anexo
            {
                ID = _id,
                Descricao = _descricao,
                DataCriacao = _datacriacao,
                Ativo = _ativo,
                Obrigatorio = _obrigatorio,
            };
            return anexo;
        }

        public AnexoBuilder WithId(Guid id)
        {
            _id = id;
            return this;
        }

        public AnexoBuilder WithDescricao(string descricao)
        {
            _descricao = descricao;
            return this;
        }

        public AnexoBuilder WithDescricaoAbreviado(string descricao)
        {
            _descricaoAbreviado = descricao;
            return this;
        }

        public AnexoBuilder WithForm(int form)
        {
            _formulario = form;
            return this;
        }

        public AnexoBuilder WithFormDesc(string descricao)
        {
            _descricaoFormulario = descricao;
            return this;
        }

        public AnexoBuilder WithDataCriacao(DateTime dataCriacao)
        {
            _datacriacao = dataCriacao;
            return this;
        }

        public AnexoBuilder WithObrigatorio(bool obrigatorio)
        {
            _obrigatorio = obrigatorio;
            return this;
        }

        public AnexoBuilder WithAtivo(bool ativo)
        {
            _ativo = ativo;
            return this;
        }

        public static implicit operator Anexo(AnexoBuilder instance)
        {
            return instance.Build();
        }
    }
}
