using System;

namespace Yara.AppService.Dtos
{
    public class BuscaPropostaProrrogacaoDto
    {
        public Guid ID { get; set; }
        public Guid ContaClienteID { get; set; }
        public string Motivo { get; set; }
        public int NumeroInternoProposta { get; set; }
        public string Origem { get; set; }
        public DateTime DataCriacao { get; set; }
        public string Status { get; set; }
        public string Cliente { get; set; }
        public decimal Valor { get; set; }
        public DateTime? VigenciaLc { get; set; }
        public decimal LcAtual { get; set; }
    }
}
