using System;

namespace Yara.Domain.Entities.Procedures
{
    public class DivisaoRemessaCockPit
    {
        public Guid Id { get; set; }
        public string ClienteNome { get; set; }
        public string GerenteNome { get; set; }
        public string CtcNome { get; set; }
        public string RepresentanteNome { get; set; }
        public string Status { get; set; }
        public decimal Total { get; set; }
        public DateTime DataEntrega { get; set; }
        public string Comentario { get; set; }
        public string Responsavel { get; set; }
        public int NumeroInternoProposta { get; set; }
        public DateTime? VigenciaLc { get; set; }
        public decimal LcAtual { get; set; }
    }
}