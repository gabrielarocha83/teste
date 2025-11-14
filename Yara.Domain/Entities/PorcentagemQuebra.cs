using System;

namespace Yara.Domain.Entities
{
    public class PorcentagemQuebra:Base
    {
        public PorcentagemQuebra()
        {
            
        }
        public PorcentagemQuebra(Guid id, int porcentagem, bool ativo, Guid usuarioidcriacao, Guid? usuarioidalteracao)
        {
            ID = id;
            Porcentagem = porcentagem;
            Ativo = ativo;
            UsuarioIDCriacao = usuarioidcriacao;

            if (usuarioidalteracao.HasValue)
                DataAlteracao = DateTime.Now;
            else
                DataCriacao= DateTime.Now;
        }
        public int Porcentagem { get; set; }
        public bool Ativo { get; set; }
        
    }
}