using System;
using Yara.Domain.Entities;

namespace Yara.Data.Tests.Builder
{
    public class ContaClienteBuilder
    {
        private Guid _id = new Guid();
        private string _documento = string.Empty;
        private string _codigoprincipal = string.Empty;
        private string _nome = string.Empty;
        private string _apelido = string.Empty;
        private Guid _tipoclienteId = Guid.NewGuid();
        // private Guid _segmentoId = Guid.NewGuid();
        private DateTime _datanascimentofundacao = DateTime.Now;
        private string _contato = string.Empty;
        private string _cep = string.Empty;
        private string _endereco = string.Empty;
        private string _cidade = string.Empty;
        private string _uf = string.Empty;
        private string _telefone = string.Empty;
        private string _email = string.Empty;
        private bool _bloqueioManual = false;
        private Guid _usuarioIDcriacao = Guid.NewGuid();
        private Guid? _usuarioIDalteracao = Guid.NewGuid();
        private DateTime _datacriacao = DateTime.Now;
        private DateTime? _dataalteracao = DateTime.Now;

        public ContaCliente Build()
        {
            var conta = new ContaCliente();
            conta.ID = _id;
            conta.Documento = _documento;
            conta.CodigoPrincipal = _codigoprincipal;
            conta.Nome = _nome;
            conta.Apelido = _apelido;
            conta.TipoClienteID = _tipoclienteId;
            conta.DataNascimentoFundacao = _datanascimentofundacao;
            conta.Contato = _contato;
            conta.CEP = _cep;
            conta.Endereco = _endereco;
            conta.Cidade = _cidade;
            conta.UF = _uf;
            conta.Telefone = _telefone;
            conta.Email = _email;
            conta.UsuarioIDAlteracao = _usuarioIDalteracao;
            conta.DataCriacao = _datacriacao;
            conta.DataAlteracao = _dataalteracao;
            conta.UsuarioIDCriacao = _usuarioIDcriacao;
            conta.BloqueioManual = _bloqueioManual;

            return conta;
        }

        public ContaClienteBuilder WithId(Guid id)
        {
            _id = id;
            return this;
        }

        public ContaClienteBuilder WithTipoClienteId(Guid id)
        {
            _tipoclienteId = id;
            return this;
        }

        public ContaClienteBuilder WithNome(string nome)
        {
            _nome = nome;
            return this;
        }

        public ContaClienteBuilder WithDocumento(string documento)
        {
            _documento = documento;
            return this;
        }

        public ContaClienteBuilder WithCodigoPrincipal(string codigo)
        {
            _codigoprincipal = codigo;
            return this;
        }

        public ContaClienteBuilder WithApelido(string apelido)
        {
            _apelido = apelido;
            return this;
        }

        public ContaClienteBuilder WithContato(string contato)
        {
            _contato = contato;
            return this;
        }

        public ContaClienteBuilder WithTelefone(string telefone)
        {
            _telefone = telefone;
            return this;
        }

        public ContaClienteBuilder WithEmail(string email)
        {
            _email = email;
            return this;
        }

        public ContaClienteBuilder WithCEP(string cep)
        {
            _cep = cep;
            return this;
        }

        public ContaClienteBuilder WithEndereco(string endereco)
        {
            _endereco = endereco;
            return this;
        }

        public ContaClienteBuilder WithCidade(string cidade)
        {
            _cidade = cidade;
            return this;
        }

        public ContaClienteBuilder WithUF(string uf)
        {
            _uf = uf;
            return this;
        }

        public ContaClienteBuilder WithDataCriacao(DateTime datacriacao)
        {
            _datacriacao = datacriacao;
            return this;
        }

        public ContaClienteBuilder WithDataNascimentoFundacao(DateTime fundacao)
        {
            _datanascimentofundacao = fundacao;
            return this;
        }

        public ContaClienteBuilder WithUsuarioIDCriacao(Guid usuarioIDCriacao)
        {
            _usuarioIDcriacao = usuarioIDCriacao;
            return this;
        }

        public ContaClienteBuilder WithUsuarioIDAlteracao(Guid usuarioIDAlteracao)
        {
            _usuarioIDalteracao = usuarioIDAlteracao;
            return this;
        }

        public ContaClienteBuilder WitDataAlteracao(DateTime dataAlteracao)
        {
            _dataalteracao = dataAlteracao;
            return this;
        }

        public ContaClienteBuilder WithBloqueioManual(bool status)
        {
            _bloqueioManual = status;
            return this;
        }

        public static implicit operator ContaCliente(ContaClienteBuilder instance)
        {
            return instance.Build();
        }
    }
}