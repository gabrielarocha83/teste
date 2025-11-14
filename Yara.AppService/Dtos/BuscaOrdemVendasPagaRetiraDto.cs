using System;

namespace Yara.AppService.Dtos
{
    public class BuscaOrdemVendasPagaRetiraDto
    {
        public Guid ClienteId { get; set; }
        public string EmpresaId { get; set; }
        public string OrdemVendaNumero { get; set; }
        public int ItemOrdemVendaItem { get; set; }
        public int Divisao { get; set; }
        public DateTime? DataRemessa { get; set; }
        public string Pagador { get; set; }
        public string Payt { get; set; }
        public string Material { get; set; }
        public string Centro { get; set; }
        public decimal VolumeSaldo { get; set; }
        public decimal ValorSaldo { get; set; }
        public decimal QtdPedida { get; set; }
        public DateTime? DataEfetivaFixa { get; set; }
        public string Bloqueio { get; set; }
        public DateTime? DataValidade { get; set; }
        public DateTime DataEmissao { get; set; }
        public decimal ValorUnitario { get; set; }
        public string BloqueioPortal { get; set; }
        public string Descricao { get; set; }
        public string DescricaoMaterial { get; set; }
        public string OutrosBloqueios { get; set; }
        public string Moeda { get; set; }
        public decimal ValorEmMoeda { get; set; }
        public bool BloqueioCarregamento { get; set; }
        public string MotivoB7 { get; set; }

        public decimal TotalEmMoeda
        {
            get
            {
                return this.ValorEmMoeda * this.QtdPedida;
            }
        }
    }
}
