using System;

namespace Yara.AppService.Dtos
{
    public class TribunalJusticaDto
    {
        public Guid ContaClienteID { get; set; }
        public string Documento { get; set; }
        public DateTime DataCriacao { get; set; }
    }
}
