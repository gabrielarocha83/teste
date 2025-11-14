using System;
using Yara.Domain.Entities;

namespace Yara.Data.Tests.Builder
{
    public class TipoPecuariaBuilder
    {
        private Guid _id = Guid.NewGuid();
        private string _tipo = "Carne";
        private string _medida = "kilo";
        private DateTime _datacriacao = DateTime.Now;
        private bool _ativo = true;
        private Guid _usuarioidcriacao = Guid.NewGuid();
        private Guid? _usuarioidalteracao = Guid.NewGuid();
        public TipoPecuaria Build()
        {
            var tipoPecuaria = new TipoPecuaria()
            {
                ID = _id,
                Tipo = _tipo,
                UnidadeMedida = _medida,
                DataCriacao = _datacriacao,
                Ativo = _ativo,
                UsuarioIDCriacao = _usuarioidcriacao,
                DataAlteracao = DateTime.Now,
                UsuarioIDAlteracao = _usuarioidalteracao
            };
            return tipoPecuaria;
        }

        public TipoPecuariaBuilder WithId(Guid id)
        {
            _id = id;
            return this;
        }

        public TipoPecuariaBuilder WithTipo(string tipo)
        {
            _tipo = tipo;
            return this;
        }

        public TipoPecuariaBuilder WithUnidadeMedida(string medida)
        {
            _medida = medida;
            return this;
        }

        public TipoPecuariaBuilder WithDataCriacao(DateTime dataCriacao)
        {
            _datacriacao = dataCriacao;
            return this;
        }
        public TipoPecuariaBuilder WithUsuarioIDCriacao(Guid usuarioIDCriacao)
        {
            _usuarioidcriacao = usuarioIDCriacao;
            return this;
        }

        public TipoPecuariaBuilder WithUsuarioIDAlteracao(Guid? usuarioIDAlteracao)
        {
            _usuarioidalteracao = usuarioIDAlteracao;
            return this;
        }
        public TipoPecuariaBuilder WithAtivo(bool ativo)
        {
            _ativo = ativo;
            return this;
        }

        public static implicit operator TipoPecuaria(TipoPecuariaBuilder instance)
        {
            return instance.Build();
        }
    }
}
