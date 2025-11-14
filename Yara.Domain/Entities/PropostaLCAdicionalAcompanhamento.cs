using System;

namespace Yara.Domain.Entities
{
    public class PropostaLCAdicionalAcompanhamento
    {
        public Guid PropostaLCAdicionalID { get; set; }
        public Guid UsuarioID { get; set; }
        public DateTime DataCriacao { get; set; }
        public bool Ativo { get; set; }
        
        public virtual PropostaLCAdicional PropostaLCAdicional { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
