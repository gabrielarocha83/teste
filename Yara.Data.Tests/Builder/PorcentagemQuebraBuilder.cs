using System;
using Yara.Domain.Entities;

namespace Yara.Data.Tests.Builder
{
    public class PorcentagemQuebraBuilder
    {
        private Guid _id = Guid.NewGuid();
        private int _porcentagem = 0;
        private DateTime _datacriacao = DateTime.Now;
        private bool _ativo = true;
        private Guid _usuarioidcriacao = Guid.NewGuid();
        private Guid? _usuarioidalteracao = Guid.NewGuid();
        public PorcentagemQuebra Build()
        {
            var porcentagemQuebra = new PorcentagemQuebra()
            {
                ID = _id,
               Porcentagem = _porcentagem,
                DataCriacao = _datacriacao,
                Ativo = _ativo,
                UsuarioIDCriacao = _usuarioidcriacao,
                DataAlteracao = DateTime.Now,
                UsuarioIDAlteracao = _usuarioidalteracao
            };
            return porcentagemQuebra;
        }

        public PorcentagemQuebraBuilder WithId(Guid id)
        {
            _id = id;
            return this;
        }

        public PorcentagemQuebraBuilder WithPorcentagem(int porcentagem)
        {
            _porcentagem = porcentagem;
            return this;
        }

        public PorcentagemQuebraBuilder WithDataCriacao(DateTime dataCriacao)
        {
            _datacriacao = dataCriacao;
            return this;
        }
        public PorcentagemQuebraBuilder WithUsuarioIDCriacao(Guid usuarioIDCriacao)
        {
            _usuarioidcriacao = usuarioIDCriacao;
            return this;
        }

        public PorcentagemQuebraBuilder WithUsuarioIDAlteracao(Guid? usuarioIDAlteracao)
        {
            _usuarioidalteracao = usuarioIDAlteracao;
            return this;
        }
        public PorcentagemQuebraBuilder WithAtivo(bool ativo)
        {
            _ativo = ativo;
            return this;
        }

        public static implicit operator PorcentagemQuebra(PorcentagemQuebraBuilder instance)
        {
            return instance.Build();
        }
    }
}
