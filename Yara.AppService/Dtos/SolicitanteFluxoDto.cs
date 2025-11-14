using System;
using System.Collections.Generic;

namespace Yara.AppService.Dtos
{
    public class SolicitanteFluxoDto : BaseDto
    {
        public string Comentario { get; set; }
        public DateTime DataSolicitacao { get; set; }
        public string Usuario { get; set; }
        public bool AcompanharProposta { get; set; }
        public string EmpresasId { get; set; }
        public EmpresasDto Empresas { get; set; }
        public decimal Total { get; set; }
        public Guid ContaClienteID { get; set; }
        public string NomeSolicitante { get; set; }

        public decimal Exposicao { get; set; }
        public decimal ExposicaoFutura => Total + Exposicao;

        public string NomeCliente { get; set; }
        public string ApelidoCliente { get; set; }
        public string DocumentoCliente { get; set; }
        public string CodigoCliente { get; set; }
        public int PendenciaSerasa { get; set; }
        public bool Conceito { get; set; }
        public string DescricaoConceito { get; set; }
        public bool BloqueioManual { get; set; }
        public string NomeCtc { get; set; }
        public string NomeSupervisor { get; set; }
        public string NomeDiretoria { get; set; }
        public bool DividaAtiva { get; set; }
        public decimal LC { get; set; }
        public DateTime? Vigencia { get; set; }
        public DateTime? VigenciaFim { get; set; }


        public List<BuscaOrdemVendasAVistaDto> Ordens { get; set; }
    }
}
