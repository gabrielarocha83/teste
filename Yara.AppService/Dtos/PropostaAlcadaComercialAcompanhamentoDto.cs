using System;

namespace Yara.AppService.Dtos
{
    public class PropostaAlcadaComercialAcompanhamentoDto
    {
        public Guid PropostaAlcadaComercialID { get; set; }
        public Guid UsuarioID { get; set; }

        public PropostaAlcadaComercialDto PropostaAlcadaComercial { get; set; }
        public UsuarioDto Usuario { get; set; }
        
        public DateTime DataCriacao { get; set; }
        public bool Ativo { get; set; }
    }
}
