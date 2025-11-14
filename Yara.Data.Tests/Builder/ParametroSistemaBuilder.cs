using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yara.Domain.Entities;

namespace Yara.Data.Tests.Builder
{
    public class ParametroSistemaBuilder
    {
        private Guid _id = Guid.NewGuid();
        private string _grupo = "";
        private string _tipo = "";
        private string _chave = "";
        private string _valor = "";
        private DateTime _datacriacao = DateTime.Now;
        private bool _ativo = true;
        private Guid _usuarioCriacao = Guid.Empty;

        public ParametroSistema Build()
        {
            var parametroSistema = new ParametroSistema
            {
                ID = _id,
                Grupo = _grupo,
                Tipo = _tipo,
                Chave = _chave,
                Valor = _valor,
                DataCriacao = _datacriacao,
                UsuarioIDCriacao = _usuarioCriacao,
                Ativo = _ativo
            };
            return parametroSistema;
        }

        public ParametroSistemaBuilder WithId(Guid id)
        {
            _id = id;
            return this;
        }

        public ParametroSistemaBuilder WithGrupo(string grupo)
        {
            _grupo = grupo;
            return this;
        }

        public ParametroSistemaBuilder WithTipo(string tipo)
        {
            _tipo = tipo;
            return this;
        }

        public ParametroSistemaBuilder WithChave(string chave)
        {
            _chave = chave;
            return this;
        }

        public ParametroSistemaBuilder WithValor(string valor)
        {
            _valor = valor;
            return this;
        }
        
        public ParametroSistemaBuilder WithDataCriacao(DateTime dataCriacao)
        {
            _datacriacao = dataCriacao;
            return this;
        }

        public ParametroSistemaBuilder WithDataUsuarioCriacao(Guid user)
        {
            _usuarioCriacao = user;
            return this;
        }

        public ParametroSistemaBuilder WithAtivo(bool ativo)
        {
            _ativo = ativo;
            return this;
        }

        public static implicit operator ParametroSistema(ParametroSistemaBuilder instance)
        {
            return instance.Build();
        }
    }
}
