using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yara.Domain.Entities;

namespace Yara.Data.Tests.Builder
{
    public class FeriadoBuilder
    {
        private Guid _id = Guid.NewGuid();
        private DateTime _dataFeriado = Convert.ToDateTime("2017-01-01 00:00:00");
        private string _descricao = "Ano Novo";
        
        private DateTime _datacriacao = DateTime.Now;
        private Guid _usuarioidcriacao = Guid.NewGuid();
        private Guid? _usuarioidalteracao = Guid.NewGuid();

        public Feriado Build()
        {
            var feriado = new Feriado()
            {
                ID = _id,
                DataFeriado = _dataFeriado,
                Descricao = _descricao,
                DataCriacao = _datacriacao,
                UsuarioIDCriacao = _usuarioidcriacao,
                DataAlteracao = DateTime.Now,
                UsuarioIDAlteracao = _usuarioidalteracao
            };
            return feriado;
        }

        public FeriadoBuilder WithId(Guid id)
        {
            _id = id;
            return this;
        }

        public FeriadoBuilder WithDescricao(string descricao)
        {
            _descricao = descricao;
            return this;
        }

        public FeriadoBuilder WithDataFeriado(DateTime data)
        {
            _dataFeriado = data;
            return this;
        }

        public FeriadoBuilder WithDataCriacao(DateTime dataCriacao)
        {
            _datacriacao = dataCriacao;
            return this;
        }

        public FeriadoBuilder WithUsuarioIdCriacao(Guid usuarioIdCriacao)
        {
            _usuarioidcriacao = usuarioIdCriacao;
            return this;
        }

        public FeriadoBuilder WithUsuarioIdAlteracao(Guid? usuarioIdAlteracao)
        {
            _usuarioidalteracao = usuarioIdAlteracao;
            return this;
        }

        public static implicit operator Feriado(FeriadoBuilder instance)
        {
            return instance.Build();
        }
    }
}
