using System;
using System.Collections.Generic;

namespace Yara.Domain.Entities
{
    public class ItemOrdemVenda
    {

        public int Item { get; set; }

        public string OrdemVendaNumero { get; set; }
        public OrdemVenda OrdemVenda { get; set; }
        
        public string Material { get; set; }
        public string Centro { get; set; }
        public string Deposito { get; set; }
        public string CondPagto { get; set; }
        public string CondFrete { get; set; }
        public string Moeda { get; set; }
        public decimal? TaxaCambio { get; set; }
        public DateTime? DataEfetivaFixa { get; set; }
        public string MotivoRecusa { get; set; }
        public decimal QtdPedida { get; set; }
        public decimal QtdEntregue { get; set; }
        public decimal QtdDelivery { get; set; }
        public decimal QtdVendaOrdem { get; set; }
        public decimal QtdExportacao { get; set; }
        public string UnidadeMedida { get; set; }
        public decimal ValorUnitario { get; set; }
        public decimal ValorEmMoeda { get; set; }
        public decimal CotacaoMoeda { get; set; }
        public bool PagaRetira { get; set; }
        public string DescricaoMaterial { get; set; }
        public string OutrosBloqueios { get; set; }
        public string CodigoCulturaSAP { get; set; }
        public string DesricaoCultura { get; set; }

        public ICollection<DivisaoRemessa> DivisaoRemessas { get; set; }
        
    }
}
