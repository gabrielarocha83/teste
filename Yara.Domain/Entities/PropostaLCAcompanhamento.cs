using System;

namespace Yara.Domain.Entities
{
    public class PropostaLCAcompanhamento
    {
        public PropostaLC PropostaLc { get; set; }
        public Guid PropostaLCID { get; set; }
        public Usuario Usuario { get; set; }
        public Guid UsuarioID { get; set; }
        public DateTime DataCriacao { get; set; }
        public bool Ativo { get; set; }
    }
}