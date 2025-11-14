namespace Yara.Domain.Entities.Procedures
{
    public class BuscaPropostaLCPorStatus
    {
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public string GC { get; set; }
        public string CTC { get; set; }
        public string Representante { get; set; }
        public string Status { get; set; }
        public string PropostaLCStatusID { get; set; }
        public decimal Valor { get; set; }
        public int LeadTime { get; set; }
        public string Responsavel { get; set; }
        public string AlcadaAnalise { get; set; }
        public string AlcadaAprovacao { get; set; }
    }
}