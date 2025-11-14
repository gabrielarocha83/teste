using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yara.Domain.Entities
{
    public class MonitorInfoGraficoProcessamento
    {

        public DateTime DataHora { get; set; }
        public decimal TempoMedioEmMinutos { get; set; }
        public int DivisoesProcessadas { get; set; }

    }
}
