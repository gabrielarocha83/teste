using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yara.Domain.Entities
{
    public class PropostaLCParceriaAgricola
    {
        // Properties
        public Guid ID { get; set; }
        public Guid PropostaLCID { get; set; }
        public string Documento { get; set; }
        public string InscricaoEstadual { get; set; }
        public string Nome { get; set; }
        public Guid? SolicitanteSerasaID { get; set; }
        public SolicitanteSerasa SolicitanteSerasa { get; set; }
        public TipoSerasa TipoSerasaID { get; set; }
        public bool RestricaoSerasa { get; set; }

        // Navigation Properties
        public PropostaLC PropostaLC { get; set; }
    }
}
