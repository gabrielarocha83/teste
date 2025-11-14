using System;
using Yara.Domain.Entities;

namespace Yara.Data.Tests.Builder
{
    public class ContaClienteComentarioBuilder
    {
        private Guid _id = Guid.NewGuid();
        Guid _usuarioId = Guid.NewGuid();
        Guid _contaClienteId = Guid.NewGuid();
        private string _descricao = "Comentario para teste de tabela de ContaClienteComentario";
        private bool _ativo = true;
        private DateTime _datacriacao = DateTime.Now;

        public ContaClienteComentario Build()
        {
            var conta = new ContaClienteComentario();
            conta.ID = _id;
            conta.Ativo = _ativo;
            conta.UsuarioID =  _usuarioId;
            conta.ContaClienteID = _contaClienteId;
            conta.Descricao = _descricao;
            conta.DataCriacao = _datacriacao;
            conta.Usuario = null;

            return conta;
        }

        public ContaClienteComentarioBuilder WithId(Guid id)
        {
            _id = id;
            return this;
        }

        public ContaClienteComentarioBuilder WithAtivo(bool status)
        {
            _ativo = status;
            return this;
        }

        public ContaClienteComentarioBuilder WithUsuarioId(Guid id)
        {
            _usuarioId = id;
            return this;
        }

        public ContaClienteComentarioBuilder WithContaClienteId(Guid id)
        {
            _contaClienteId = id;
            return this;
        }

        public ContaClienteComentarioBuilder WithDescricao(string descricao)
        {
            _descricao = descricao;
            return this;
        }

        public ContaClienteComentarioBuilder WithDataCriacao(DateTime dtCriacao)
        {
            _datacriacao = dtCriacao;
            return this;
        }
    }
}
