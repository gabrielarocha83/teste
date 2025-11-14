using System;

namespace Yara.Domain.Entities
{
    public class PropostaJuridicoHistoricoPagamento
    {
        public Guid ID { get; set; }
        public Guid PropostaJuridicoID { get; set; }
        public DateTime DataPagamento { get; set; }
        public string OrdemVendaNumero { get; set; }
        public string NotaFiscal { get; set; }
        public decimal ValorDocumento { get; set; }

        public PropostaJuridico PropostaJuridico { get; set; }
    }
}
