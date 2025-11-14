using System;
using Yara.Domain.Entities;

namespace Yara.Data.Tests.Builder
{
    public class AreaIrrigadaBuilder
    {
        private Guid _id = Guid.NewGuid();
        private string _nome = "Tipo AreaIrrigada de Teste";
        private DateTime _datacriacao = DateTime.Now;
        private bool _ativo = true;

        public AreaIrrigada Build()
        {
            var AreaIrrigada = new AreaIrrigada
            {
                ID = _id,
                Nome = _nome,
                DataCriacao = _datacriacao,
                Ativo = _ativo
            };
            return AreaIrrigada;
        }

        public AreaIrrigadaBuilder WithId(Guid id)
        {
            _id = id;
            return this;
        }

        public AreaIrrigadaBuilder WithNome(string nome)
        {
            _nome = nome;
            return this;
        }

        public AreaIrrigadaBuilder WithDataCriacao(DateTime dataCriacao)
        {
            _datacriacao = dataCriacao;
            return this;
        }

        public AreaIrrigadaBuilder WithAtivo(bool ativo)
        {
            _ativo = ativo;
            return this;
        }

        public static implicit operator AreaIrrigada(AreaIrrigadaBuilder instance)
        {
            return instance.Build();
        }
    }
}
