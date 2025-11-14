using System;

namespace Yara.Domain.Entities
{
    public class PropostaAlcadaComercialAcompanhamento
    {
        public PropostaAlcadaComercial PropostaAlcadaComercial { get; set; }
        public Guid PropostaAlcadaComercialID { get; set; }
        public Usuario Usuario { get; set; }
        public Guid UsuarioID { get; set; }
        public DateTime DataCriacao { get; set; }
        public bool Ativo { get; set; }
    }
}
