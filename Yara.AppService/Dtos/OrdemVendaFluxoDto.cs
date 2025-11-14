using System;

namespace Yara.AppService.Dtos
{
    public class OrdemVendaFluxoDto : BaseDto
    {
        public Guid SolicitanteFluxoID { get; set; }
        public SolicitanteFluxoDto SolicitanteFluxo { get; set; }

        public int Divisao { get; set; }
        public int ItemOrdemVenda { get; set; }
        public string OrdemVendaNumero { get; set; }

        public string EmpresasId { get; set; }
        public EmpresasDto Empresas { get; set; }

        public OrdemVendaFluxoDto()
        {
            ID = Guid.NewGuid();
            DataCriacao = DateTime.Now;
            
        }
    }
}
