using System;

namespace Yara.Domain.Entities
{
    public class PropostaAbonoAcompanhamento
    {

        public PropostaAbono PropostaAbono { get; set; }
        public Guid PropostaAbonoID { get; set; }
        public Usuario Usuario { get; set; }
        public Guid UsuarioID { get; set; }
        public DateTime DataCriacao { get; set; }
        public bool Ativo { get; set; }
    }
}