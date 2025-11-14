using System;

namespace Yara.Domain.Entities
{
    public class PropostaAlcadaComercialDocumentos
    {
        public Guid ID { get; set; }
        public string Documento { get; set; }
        public bool RestricaoSerasa { get; set; }
        public Guid? SolicitanteSerasaID { get; set; }
        public SolicitanteSerasa SolicitanteSerasa { get; set; }
        public Guid PropostaAlcadaComercialID { get; set; }
        public TipoSerasa TipoSerasaID { get; set; }
        public PropostaAlcadaComercial PropostaAlcadaComercial { get; set; }
    }
}