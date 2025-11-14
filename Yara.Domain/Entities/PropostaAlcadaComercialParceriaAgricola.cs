using System;

namespace Yara.Domain.Entities
{
    public class PropostaAlcadaComercialParceriaAgricola
    {
        // Properties
        public Guid ID { get; set; }
        public Guid PropostaAlcadaComercialID { get; set; }
        public string Documento { get; set; }
        public string InscricaoEstadual { get; set; }
        public string Nome { get; set; }
        public Guid? SolicitanteSerasaID { get; set; }
        public SolicitanteSerasa SolicitanteSerasa { get; set; }
        public TipoSerasa TipoSerasaID { get; set; }
        public bool RestricaoSerasa { get; set; }

        // Navigation Properties
        public PropostaAlcadaComercial PropostaAlcadaComercial { get; set; }
    }
}