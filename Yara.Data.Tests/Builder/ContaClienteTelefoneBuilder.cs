using System;
using Yara.AppService.Dtos;

namespace Yara.Data.Tests.Builder
{
    public class ContaClienteTelefoneBuilder
    {
        private Guid _id = Guid.NewGuid();
        Guid _usuarioIdcriacao = Guid.NewGuid();
        Guid? _usuarioIdalteracao = Guid.NewGuid();
        Guid _contaClienteId = Guid.NewGuid();
        private string _telefone = "";
        private TipoTelefoneDto _tipo = TipoTelefoneDto.Fixo;
        private bool _ativo = true;
        private DateTime _datacriacao = DateTime.Now;
        private DateTime? _dataalteracao = DateTime.Now;

        public ContaClienteTelefoneDto Build()
        {
            var contaClienteTelefone = new ContaClienteTelefoneDto
            {
                ID = _id,
                Ativo = _ativo,
                UsuarioIDCriacao = _usuarioIdcriacao,
                ContaClienteID = _contaClienteId,
                Telefone = _telefone,
                Tipo = _tipo,
                UsuarioIDAlteracao = _usuarioIdalteracao,
                DataAlteracao = _dataalteracao,
                DataCriacao = _datacriacao
            };

            return contaClienteTelefone;
        }

        public ContaClienteTelefoneBuilder WithId(Guid id)
        {
            _id = id;
            return this;
        }

        public ContaClienteTelefoneBuilder WithAtivo(bool status)
        {
            _ativo = status;
            return this;
        }

        public ContaClienteTelefoneBuilder WithUsuarioIdCriacao(Guid id)
        {
            _usuarioIdcriacao = id;
            return this;
        }

        public ContaClienteTelefoneBuilder WithUsuarioIdAlteracao(Guid id)
        {
            _usuarioIdalteracao= id;
            return this;
        }

        public ContaClienteTelefoneBuilder WithContaClienteId(Guid id)
        {
            _contaClienteId = id;
            return this;
        }

        public ContaClienteTelefoneBuilder WithTelefone(string telefone)
        {
            _telefone = telefone;
            return this;
        }

        public ContaClienteTelefoneBuilder WithDataCriacao(DateTime dtCriacao)
        {
            _datacriacao = dtCriacao;
            return this;
        }

        public ContaClienteTelefoneBuilder WithDataAlteracao(DateTime dtAlteracao)
        {
            _dataalteracao = dtAlteracao;
            return this;
        }

        public static implicit operator ContaClienteTelefoneDto(ContaClienteTelefoneBuilder instance)
        {
            return instance.Build();
        }
    }
}
