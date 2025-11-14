using System;

namespace Yara.Domain.Entities.Procedures
{
    public class BuscaPropostaLCContaCliente
    {
        public Guid ID { get; set; }
        public DateTime DataCriacao { get; set; }
        public string Codigo { get; set; }
        public string CTC { get; set; }
        public string GC { get; set; }
        public string DR { get; set; }
        public string Representante { get; set; }
        public string Status { get; set; }
        public decimal Valor { get; set; }
        public decimal? Aprovado { get; set; }
        public string Responsavel { get; set; }
    }
}