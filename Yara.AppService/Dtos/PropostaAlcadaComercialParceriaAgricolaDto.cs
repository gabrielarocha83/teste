using System;

namespace Yara.AppService.Dtos
{
    public class PropostaAlcadaComercialParceriaAgricolaDto
    {
        // Properties
        public Guid ID { get; set; }
        public Guid PropostaAlcadaComercialID { get; set; }
        public string Documento { get; set; }
        public string InscricaoEstadual { get; set; }
        public string Nome { get; set; }
        public Guid? SolicitanteSerasaID { get; set; }
        public bool RestricaoSerasa { get; set; }
    }
}