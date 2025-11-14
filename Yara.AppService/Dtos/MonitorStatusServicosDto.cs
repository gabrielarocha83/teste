using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yara.AppService.Dtos
{
    public class MonitorStatusServicosDto
    {

        public bool ServicoPortal { get; set; }
        public bool ConexaoSAP { get; set; }
        public bool ConexaoPI { get; set; }

    }
}
