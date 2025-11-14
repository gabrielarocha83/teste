using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yara.Domain.Entities;

namespace Yara.Data.Tests.Builder
{
    public class ProdutoServicoBuilder
    {
        private Guid _id = Guid.NewGuid();
        private string _nome = "Produto ou Serviço de Teste";
        private DateTime _datacriacao = DateTime.Now;
        private bool _ativo = true;
        private Guid _usuarioCriacao = Guid.Empty;

        public ProdutoServico Build()
        {
            var produtoServico = new ProdutoServico
            {
                ID = _id,
                Nome = _nome,
                DataCriacao = _datacriacao,
                Ativo = _ativo
            };
            return produtoServico;
        }

        public ProdutoServicoBuilder WithId(Guid id)
        {
            _id = id;
            return this;
        }

        public ProdutoServicoBuilder WithNome(string nome)
        {
            _nome = nome;
            return this;
        }

        public ProdutoServicoBuilder WithDataCriacao(DateTime dataCriacao)
        {
            _datacriacao = dataCriacao;
            return this;
        }

        public ProdutoServicoBuilder WithAtivo(bool ativo)
        {
            _ativo = ativo;
            return this;
        }

        public ProdutoServicoBuilder WithUsuarioCriacao(Guid id)
        {
            _usuarioCriacao = id;
            return this;
        }

        public static implicit operator ProdutoServico(ProdutoServicoBuilder instance)
        {
            return instance.Build();
        }
    }
}
