using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yara.AppService.Dtos
{
    public class MonitorQuantidadesFilasDto
    {

        public int OrdensAguardandoEnvioPortal { get; set; }
        public int OrdensAguardandoDownload { get; set; }
        public int OrdensAguardandoProcessamento { get; set; }
        public int DivisoesAguardandoEnvioSAP { get; set; }

    }
}
