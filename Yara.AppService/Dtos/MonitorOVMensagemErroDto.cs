using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yara.AppService.Dtos
{
    public class MonitorOVMensagemErroDto
    {
        public DateTime DataHora { get; set; }
        public string Mensagem { get; set; }
    }
}
