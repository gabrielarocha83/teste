using System;
using Yara.Domain.Entities;

namespace Yara.Data.Tests.Builder
{
    public class TipoEmpresaBuilder
    {
        private Guid _id = Guid.NewGuid();
        private string _tipo = "Tipo Empresa de Teste";
        private DateTime _datacriacao = DateTime.Now;
        private bool _ativo = true;

        public TipoEmpresa Build()
        {
            var tipoEmpresa = new TipoEmpresa
            {
                ID = _id,
                Tipo = _tipo,
                DataCriacao = _datacriacao,
                Ativo = _ativo
            };
            return tipoEmpresa;
        }

        public TipoEmpresaBuilder WithId(Guid id)
        {
            _id = id;
            return this;
        }

        public TipoEmpresaBuilder WithTipo(string tipo)
        {
            _tipo = tipo;
            return this;
        }

        public TipoEmpresaBuilder WithDataCriacao(DateTime dataCriacao)
        {
            _datacriacao = dataCriacao;
            return this;
        }
        
        public TipoEmpresaBuilder WithAtivo(bool ativo)
        {
            _ativo = ativo;
            return this;
        }

        public static implicit operator TipoEmpresa(TipoEmpresaBuilder instance)
        {
            return instance.Build();
        }
        
    }
}
