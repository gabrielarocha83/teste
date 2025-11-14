using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yara.AppService.Dtos
{
    public class MonitorDataInputDto
    {

        public DateTime DataInicio { get; set; }
        public DateTime? DataFim { get; set; }

    }
}
