using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yara.Domain.Entities.Procedures
{
    public class LogEnvioOrdensSAP
    {

        public string Numero { get; set; }
        public int Item { get; set; }
        public int Divisao { get; set; }
        public bool Bloqueada { get; set; }
        public string Motivo { get; set; }
        public DateTime DataEvento { get; set; }
        public DateTime DataEnvioSAP { get; set; }
        public string Detalhes { get; set; }

    }
}
