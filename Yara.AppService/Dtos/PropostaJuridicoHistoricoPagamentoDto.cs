using System;

namespace Yara.AppService.Dtos
{
    public class PropostaJuridicoHistoricoPagamentoDto
    {
        public Guid ID { get; set; }
        public Guid PropostaJuridicoID { get; set; }
        public DateTime DataPagamento { get; set; }
        public string OrdemVendaNumero { get; set; }
        public string NotaFiscal { get; set; }
        public decimal ValorDocumento { get; set; }
    }
}
