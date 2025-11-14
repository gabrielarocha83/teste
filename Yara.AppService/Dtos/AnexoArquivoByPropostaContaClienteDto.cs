using System;

namespace Yara.AppService.Dtos
{
    public class AnexoArquivoByPropostaContaClienteDto
    {
        public string LayoutProposta { get; set; }
        public Guid PropostaId { get; set; }
        public Guid ContaClienteId { get; set; }
    }
}
