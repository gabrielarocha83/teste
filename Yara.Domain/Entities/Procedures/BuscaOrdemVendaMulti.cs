using System;

namespace Yara.Domain.Entities.Procedures
{
    public class BuscaOrdemVendaMulti
    {
        public string OrdemVendaNumero { get; set; }
        public int ItemOrdemVendaItem { get; set; }
        public int Divisao { get; set; }
        public DateTime DataRemessa { get; set; }
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
    }
}
