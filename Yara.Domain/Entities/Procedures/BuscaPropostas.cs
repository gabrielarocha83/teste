using System;

namespace Yara.Domain.Entities.Procedures
{
    public class BuscaPropostas
    {
        public Guid ID { get; set; }
        public Guid ContaClienteID { get; set; }
        public string Documento { get; set; }
        public string TipoProposta { get; set; }
        public string CodigoProposta { get; set; }
        public string NomeCliente { get; set; }
        public string CodigoCliente { get; set; }
        public decimal? LCDisponivel { get; set; }
        public DateTime? DataEntrega { get; set; }
        public string TipoCliente { get; set; }
        public string Status { get; set; }
        public string Responsavel { get; set; }
        public string Analista { get; set; }
        public string CTC { get; set; }
        public string GC { get; set; }
        public string Diretoria { get; set; }
        public Guid SegmentoID { get; set; }
        public string Segmento { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataConclusao { get; set; }
        public decimal? ValorProposta { get; set; }
        public string Rating { get; set; }
        public DateTime? Vigencia { get; set; }
        public DateTime? VigenciaFim { get; set; }
        public decimal? LCAprovado { get; set; }
        public int LeadTime { get; set; }
    }
}
