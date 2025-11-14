using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yara.AppService.Dtos
{
    public class ContaClienteResumoCobrancaDto
    {

        public decimal? Pdd { get; set; }
        public bool Sinistro { get; set; }
        public DateTime? DataSeguradora { get; set; }
        public DateTime? UltimaVisitaSolicitada { get; set; }
        public DateTime? UltimaVisitaRealizada { get; set; }
        public string PrincipaisCulturas { get; set; }

    }
}
