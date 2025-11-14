using System;

namespace Yara.Domain.Entities
{
    public class ProcessamentoCarteira
    {
        public Guid ID { get; set; }
        public string EmpresaID { get; set; }
        public string Cliente { get; set; }
        public DateTime DataHora { get; set; }
        public int Status { get; set; }
        public string Motivo { get; set; }
        public string Detalhes { get; set; }
        public string OrdemVenda { get; set; }
        public Guid? SolicitanteFluxoID { get; set; }
        public SolicitanteFluxo SolicitanteFluxo { get; set; }
    }
}