using System;
using Yara.Domain.Entities;

namespace Yara.Data.Tests.Builder
{
    public class SegmentoBuilder
    {
        private Guid _id = Guid.NewGuid();
        private string _descricao = "Segmento de Teste";
        private bool _ativo = true;
        Guid _usuarioIdCriacao = Guid.NewGuid();
        Guid _usuarioIdAlteracao = Guid.NewGuid();
        private DateTime _datacriacao = DateTime.Now;
        //private DateTime? _dataalteracao = null;

        public Segmento Build()
        {
            var segmento = new Segmento();
            segmento.ID = _id;
            segmento.Ativo = _ativo;
            segmento.UsuarioIDCriacao = _usuarioIdCriacao;
            segmento.UsuarioIDAlteracao = _usuarioIdAlteracao;
            segmento.Descricao = _descricao;
            segmento.DataCriacao = _datacriacao;

            return segmento;
        }

        public SegmentoBuilder WithId(Guid id)
        {
            _id = id;
            return this;
        }

        public SegmentoBuilder WithAtivo(bool status)
        {
            _ativo = status;
            return this;
        }

        public SegmentoBuilder WithUsuarioIdCriacao(Guid id)
        {
            _usuarioIdCriacao = id;
            return this;
        }

        public SegmentoBuilder WithUsuarioIdAlteracao(Guid id)
        {
            _usuarioIdAlteracao = id;
            return this;
        }

        public SegmentoBuilder WithDescricao(string descricao)
        {
            _descricao = descricao;
            return this;
        }

        public SegmentoBuilder WithDataCriacao(DateTime dtCriacao)
        {
            _datacriacao = dtCriacao;
            return this;
        }

    }
}
