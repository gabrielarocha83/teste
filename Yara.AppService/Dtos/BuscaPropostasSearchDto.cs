using System;

namespace Yara.AppService.Dtos
{
    public class BuscaPropostasSearchDto
    {
        public string TipoProposta { get; set; }
        public string CodigoProposta { get; set; }
        public string NomeCliente { get; set; }
        public string Status { get; set; }
        public decimal? ValorProposta { get; set; }
        public DateTime? Vigencia { get; set; }
        public DateTime? VigenciaFim { get; set; }
        public string Rating { get; set; }
        public string TipoCliente { get; set; }
        public string Segmento { get; set; }
        public string Diretoria { get; set; }
        public string GC { get; set; }
        public string CTC { get; set; }
        public DateTime? DataCriacao { get; set; }
        public DateTime? DataCriacaoFim { get; set; }
        public DateTime? DataConclusao { get; set; }
        public DateTime? DataConclusaoFim { get; set; }
        public DateTime? DataEntregue { get; set; }
        public DateTime? DataEntregueFim { get; set; }
        public string Analista { get; set; }
        public string Documento { get; set; }
        public string EmpresaID { get; set; }
        public int? LeadTime { get; set; }
        public int? LeadTimeFim { get; set; }
    }
}
