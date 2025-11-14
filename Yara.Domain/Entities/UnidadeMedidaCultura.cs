using System;

namespace Yara.Domain.Entities
{
    public class UnidadeMedidaCultura:Base
    {
        public UnidadeMedidaCultura()
        {
            
        }
        public UnidadeMedidaCultura(Guid id, string nome, string sigla, bool ativo, Guid usuarioidcriacao, Guid? usuarioidalteracao)
        {
            ID = id;
            Nome = nome;
            Sigla = sigla;
            Ativo = ativo;
            DataCriacao = DateTime.Now;
            UsuarioIDCriacao = usuarioidcriacao;
            if (usuarioidalteracao.HasValue)
            {
                DataAlteracao = DateTime.Now;
                UsuarioIDAlteracao = usuarioidalteracao;
            }
        }
        public string Nome { get; set; }
        public string Sigla { get; set; }
        public bool Ativo { get; set; }
        
    }
}