using System.Collections.Generic;

namespace Yara.Domain.Entities.Procedures
{
    public class BuscaHistoricoGrupo
    {

        public int Ano { get; set; }
        public decimal Montante { get; set; }
        public decimal MontantePrazo { get; set; }
        public decimal MontanteAVista { get; set; }
        public bool Atraso { get; set; }
        public List<string> ClientesAtraso { get; set; }
        public bool Pefin { get; set; }
        public List<string> ClientesPefin { get; set; }
        public bool OpFinanciamento { get; set; }
        public List<string> ClientesOpFinanciamento { get; set; }

    }
}
