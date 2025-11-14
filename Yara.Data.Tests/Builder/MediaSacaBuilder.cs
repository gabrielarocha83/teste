using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yara.Domain.Entities;

namespace Yara.Data.Tests.Builder
{
    public class MediaSacaBuilder
    {
        private Guid _id = Guid.NewGuid();
        private string _nome = "Preço Media da Saca cadastrado de Teste";
        private DateTime _datacriacao = DateTime.Now;
        private bool _ativo = true;
        private Guid _usuarioCriacao;
        private decimal _valor;
        private decimal _peso;

        public MediaSaca Build()
        {
            var mediaSaca = new MediaSaca
            {
                ID = _id,
                Nome = _nome,
                DataCriacao = _datacriacao,
                UsuarioIDCriacao = _usuarioCriacao,
                Valor = _valor,
                Peso = _peso,
                Ativo = _ativo
            };
            return mediaSaca;
        }

        public MediaSacaBuilder WithId(Guid id)
        {
            _id = id;
            return this;
        }

        public MediaSacaBuilder WithNome(string nome)
        {
            _nome = nome;
            return this;
        }

        public MediaSacaBuilder WithDataCriacao(DateTime dataCriacao)
        {
            _datacriacao = dataCriacao;
            return this;
        }

        public MediaSacaBuilder WithDataUsuarioCriacao(Guid user)
        {
            _usuarioCriacao = user;
            return this;
        }

        public MediaSacaBuilder WithAtivo(bool ativo)
        {
            _ativo = ativo;
            return this;
        }

        public MediaSacaBuilder WithValor(decimal valor)
        {
            _valor = valor;
            return this;
        }

        public MediaSacaBuilder WithPeso(decimal peso)
        {
            _peso = peso;
            return this;
        }

        public static implicit operator MediaSaca(MediaSacaBuilder instance)
        {
            return instance.Build();
        }
    }
}
