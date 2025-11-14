using System.Collections.Generic;

namespace Yara.AppService.Dtos
{
    public class BuscaHistoricoGrupoDto
    {
        public int Ano { get; set; }
        public decimal Montante { get; set; }
        public decimal MontantePrazo { get; set; }
        public decimal MontanteAVista { get; set; }

        public bool Atraso { get; set; }

        public bool Pefin { get; set; }
        public bool OpFinanciamento { get; set; }

        public List<string> ClientesAtraso { get; set; }
        public List<string> ClientesPefin { get; set; }
        public List<string> ClientesOpFinanciamento { get; set; }
    }
}
