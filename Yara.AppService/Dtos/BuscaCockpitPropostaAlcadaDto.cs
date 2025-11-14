using System;

namespace Yara.AppService.Dtos
{
    public class BuscaCockpitPropostaAlcadaDto
    {

        public Guid ContaClienteId { get; set; }
        //public int NumeroProposta { get; set; }
        public int NumeroInternoProposta { get; set; }
        public string NomeCliente { get; set; }
        public string NomeGc { get; set; }
        public string NomeCtc { get; set; }
        public string NomeRepresentante { get; set; }
        public string Status { get; set; }
        public decimal? LCProposto { get; set; }
        public string Responsavel { get; set; }
        public DateTime DataCriacao { get; set; }
        public int LeadTime { get; set; }
        public DateTime? VigenciaLc { get; set; }
        public decimal LcAtual { get; set; }
        public string NumeroProposta => $"AC{NumeroInternoProposta:00000}/{DataCriacao:yyyy}";
    }
}
