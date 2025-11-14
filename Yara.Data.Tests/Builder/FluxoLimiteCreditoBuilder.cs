using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yara.Domain.Entities;

namespace Yara.Data.Tests.Builder
{
    public class FluxoLimiteCreditoBuilder
    {
        private Guid _id = Guid.NewGuid();
        private int _nivel;
        private decimal _valorde;
        private decimal _valorate;
        private Guid _usuario = Guid.Empty;
        private Guid _grupo = Guid.Empty;
        private string _estrutura = "";
        private DateTime _datacriacao = DateTime.Now;
        private Guid _usuarioCriacao = Guid.Empty;
        
        public FluxoLiberacaoManual Build()
        {
            var fluxoLimiteCredito = new FluxoLiberacaoManual
            {
                ID = _id,
                Nivel = _nivel,
                ValorDe = _valorde,
                ValorAte = _valorate,
                Usuario = _usuario,
                Grupo = _grupo,
                Estrutura = _estrutura,
                DataCriacao = _datacriacao,
                UsuarioIDCriacao = _usuarioCriacao
            };
            return fluxoLimiteCredito;
        }

        public FluxoLimiteCreditoBuilder WithId(Guid id)
        {
            _id = id;
            return this;
        }

        public FluxoLimiteCreditoBuilder WithNivel(int nivel)
        {
            _nivel = nivel;
            return this;
        }

        public FluxoLimiteCreditoBuilder WithValorDe(decimal de)
        {
            _valorde = de;
            return this;
        }

        public FluxoLimiteCreditoBuilder WithValorAte(decimal ate)
        {
            _valorate = ate;
            return this;
        }

        public FluxoLimiteCreditoBuilder WithUsuario(Guid user)
        {
            _usuario = user;
            return this;
        }

        public FluxoLimiteCreditoBuilder WithGrupo(Guid grupo)
        {
            _grupo = grupo;
            return this;
        }

        public FluxoLimiteCreditoBuilder WithEstrutura(string estrutura)
        {
            _estrutura = estrutura;
            return this;
        }

        public FluxoLimiteCreditoBuilder WithDataCriacao(DateTime dataCriacao)
        {
            _datacriacao = dataCriacao;
            return this;
        }

        public FluxoLimiteCreditoBuilder WithDataUsuarioCriacao(Guid user)
        {
            _usuarioCriacao = user;
            return this;
        }
        
        public static implicit operator FluxoLiberacaoManual(FluxoLimiteCreditoBuilder instance)
        {
            return instance.Build();
        }
    }
}
