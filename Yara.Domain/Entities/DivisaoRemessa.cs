using System;

namespace Yara.Domain.Entities
{
    public class DivisaoRemessa
    {
        public int Divisao { get; set; }
        public int ItemOrdemVendaItem { get; set; }
        public ItemOrdemVenda ItemOrdemVenda { get; set; }
        public string OrdemVendaNumero { get; set; }
        public decimal QtdPedida { get; set; }
        public decimal QtdEntregue { get; set; }
        public string UnidadeMedida { get; set; }
        public DateTime? DataRemessa { get; set; }
        public DateTime? DataOrganizacao { get; set; }
        public DateTime? DataPreparacao { get; set; }
        public DateTime? DataCarregamento { get; set; }
        public DateTime? DataSaida { get; set; }
        public string Status { get; set; }
        public string Motivo { get; set; }
        public string Bloqueio { get; set; }
        public string BloqueioPortal { get; set; }
        public bool BloqueioCarregamento { get; set; }
        public string MotivoB7 { get; set; }

        public DivisaoRemessa()
        {
            this.BloqueioCarregamento = false;
        }
    }
}
