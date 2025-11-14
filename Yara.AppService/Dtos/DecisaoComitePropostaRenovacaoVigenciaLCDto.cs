using System;

namespace Yara.AppService.Dtos
{
    public class DecisaoComitePropostaRenovacaoVigenciaLCDto
    {
        public Guid PropostaRenovacaoVigenciaLCComiteID { get; set; }
        public string StatusComiteID { get; set; }
        public string Comentario { get; set; }
        public string EmpresasID { get; set; }
    }
}
