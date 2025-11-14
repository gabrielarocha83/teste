using System;

namespace Yara.AppService.Dtos
{
    public class BuscaCockpitPropostaLCDto
    {
        public Guid ID { get; set; }
        public Guid ContaClienteID { get; set; }
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public string CTC { get; set; }
        public string GC { get; set; }
        public string DR { get; set; }
        public string Representante { get; set; }
        public string Status { get; set; }
        public decimal Valor { get; set; }
        public DateTime? Data { get; set; }
        public int LeadTime { get; set; }
        public string Responsavel { get; set; }
        public DateTime? VigenciaLc { get; set; }
        public decimal LcAtual { get; set; }
    }
}