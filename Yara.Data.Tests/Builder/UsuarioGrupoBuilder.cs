using System;
using Yara.Domain.Entities;

namespace Yara.Data.Tests.Builder
{
    public class UsuarioGrupoBuilder
    {
        private Guid _id = Guid.NewGuid();
        private Guid _idGrupo = Guid.NewGuid();
        private Guid _idUsuario = Guid.NewGuid();
        private DateTime _datacriacao = DateTime.Now;
        private DateTime? _dataalteracao = null;
        private bool _ativo = true;

        public UsuarioGrupo Build()
        {
            var usuarioGrupo = new UsuarioGrupo()
            {
                GrupoID = _idGrupo,
                UsuarioID = _idUsuario,
                DataCriacao = _datacriacao,
                DataAlteracao = _dataalteracao,
                Ativo = _ativo
            };
            return usuarioGrupo;
        }

        public UsuarioGrupoBuilder WithId(Guid id)
        {
            _id = id;
            return this;
        }

        public UsuarioGrupoBuilder WithIdUsuario(Guid id)
        {
            _idUsuario = id;
            return this;
        }

        public UsuarioGrupoBuilder WithIdGrupo(Guid id)
        {
            _idGrupo = id;
            return this;
        }

        public UsuarioGrupoBuilder WithDataCriacao(DateTime dataCriacao)
        {
            _datacriacao = dataCriacao;
            return this;
        }

        public UsuarioGrupoBuilder WithDataAlteracao(DateTime dataAltercao)
        {
            _dataalteracao = dataAltercao;
            return this;
        }

        public UsuarioGrupoBuilder WithAtivo(bool ativo)
        {
            _ativo = ativo;
            return this;
        }

        public static implicit operator UsuarioGrupo(UsuarioGrupoBuilder instance)
        {
            return instance.Build();
        }

    }
}
