using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yara.Domain.Entities
{
    public class MonitorOVNotificacao
    {

        public DateTime DataHora { get; set; }
        public string OrdemVenda { get; set; }
        public string Status { get; set; }
        public DateTime? DataHoraDownload { get; set; }
        public DateTime? DataHoraProcessamento { get; set; }
        public DateTime? DataHoraUltimoEnvio { get; set; }

    }
}
