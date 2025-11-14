using System;

namespace Yara.AppService.Dtos
{
    public class ProcessamentoCarteiraDto
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
        public SolicitanteFluxoDto SolicitanteFluxo { get; set; }
    }
}
