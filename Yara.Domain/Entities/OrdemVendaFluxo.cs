using System;

namespace Yara.Domain.Entities
{
    public class OrdemVendaFluxo : Base
    {
        public Guid SolicitanteFluxoID { get; set; }
        public SolicitanteFluxo SolicitanteFluxo { get; set; }

        public int Divisao { get; set; }
        public int ItemOrdemVenda { get; set; }
        public string OrdemVendaNumero { get; set; }

        public string EmpresasId { get; set; }
        public Empresas Empresas { get; set; }
    }
}
