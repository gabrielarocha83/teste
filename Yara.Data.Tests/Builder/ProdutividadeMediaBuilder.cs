using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yara.Domain.Entities;

namespace Yara.Data.Tests.Builder
{
    public class ProdutividadeMediaBuilder
    {
        private Guid _id = Guid.NewGuid();
        private string _nome = "Produtividade Média de Teste";
        private DateTime _datacriacao = DateTime.Now;
        private bool _ativo = true;
        private Guid _usuarioCriacao = Guid.Empty;

        public ProdutividadeMedia Build()
        {
            var produtividadeMedia = new ProdutividadeMedia
            {
                ID = _id,
                Nome = _nome,
                DataCriacao = _datacriacao,
                Ativo = _ativo
            };
            return produtividadeMedia;
        }

        public ProdutividadeMediaBuilder WithId(Guid id)
        {
            _id = id;
            return this;
        }

        public ProdutividadeMediaBuilder WithNome(string nome)
        {
            _nome = nome;
            return this;
        }

        public ProdutividadeMediaBuilder WithDataCriacao(DateTime dataCriacao)
        {
            _datacriacao = dataCriacao;
            return this;
        }

        public ProdutividadeMediaBuilder WithDataUsuarioCriacao(Guid user)
        {
            _usuarioCriacao = user;
            return this;
        }

        public ProdutividadeMediaBuilder WithAtivo(bool ativo)
        {
            _ativo = ativo;
            return this;
        }

        public static implicit operator ProdutividadeMedia(ProdutividadeMediaBuilder instance)
        {
            return instance.Build();
        }
    }
}
