using System;
using System.Collections.Generic;

namespace Yara.Domain.Entities
{
    public class IdadeMediaCanavial : Base
    {
        public IdadeMediaCanavial()
        {
            
        }

        public IdadeMediaCanavial(string nome, bool ativo, Guid usuariocriacao, Guid? usuarioalteracao)
        {
            this.Nome = nome;
            this.Ativo = ativo;
            this.DataCriacao = DateTime.Now;
            this.UsuarioIDCriacao = usuariocriacao;

            if (usuarioalteracao.HasValue)
            {
                this.DataAlteracao = DateTime.Now;
                this.UsuarioIDAlteracao = usuarioalteracao;
            }
        }

        public string Nome { get; set; }
        public bool Ativo { get; set; }

        public ICollection<PropostaLC> PropostasLC { get; set; }
    }
}