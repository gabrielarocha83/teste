using System;
using Newtonsoft.Json;

namespace Yara.AppService.Dtos
{
    public class DivisaoRemessaDto
    {
        public int Divisao { get; set; }
        public int ItemOrdemVendaItem { get; set; }
        [JsonIgnore]
        public ItemOrdemVendaDto ItemOrdemVenda { get; set; }
        public string OrdemVendaNumero { get; set; }
        //public OrdemVenda OrdemVenda { get; set; }
        public decimal QtdPedida { get; set; }
        public decimal QtdConfirmada { get; set; }
        public string UnidadeMedida { get; set; }
        public DateTime? DataOrganizacao { get; set; }
        public DateTime? DataPreparacao { get; set; }
        public DateTime? DataCarregamento { get; set; }
        public DateTime? DataSaida { get; set; }
        public string Status { get; set; }
        public string Motivo { get; set; }
        public string Bloqueio { get; set; }
        public string BloqueioPortal { get; set; }
        public bool BloqueioCarregamento { get; set; }

        public DivisaoRemessaDto()
        {
            this.BloqueioCarregamento = false;
        }
    }
}
