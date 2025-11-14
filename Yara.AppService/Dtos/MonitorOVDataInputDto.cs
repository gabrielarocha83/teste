using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yara.AppService.Dtos
{
    public class MonitorOVDataInputDto
    {

        public string OrdemVenda { get; set; }
        public DateTime? DataInicio { get; set; }
        public DateTime? DataFim { get; set; }

    }
}
