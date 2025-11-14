using System;
using Yara.AppService.Dtos;

namespace Yara.AppService.Dtos
{
    public class PropostaAlcadaComercialDocumentosDto
    {
        public Guid ID { get; set; }
        public string Documento { get; set; }
        public bool RestricaoSerasa { get; set; }
        public Guid? SolicitanteSerasaID { get; set; }
        public SolicitanteSerasaDto SolicitanteSerasa { get; set; }
        public Guid PropostaAlcadaComercialID { get; set; }
        public PropostaAlcadaComercialDto PropostaAlcadaComercial { get; set; }
    }
}