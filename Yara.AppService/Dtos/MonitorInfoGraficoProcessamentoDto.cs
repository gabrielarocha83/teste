using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yara.AppService.Dtos
{
    public class MonitorInfoGraficoProcessamentoDto
    {

        public DateTime DataHora { get; set; }
        public decimal TempoMedioEmMinutos { get; set; }
        public int DivisoesProcessadas { get; set; }

    }
}
