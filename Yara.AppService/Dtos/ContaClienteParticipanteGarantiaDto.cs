using System;

namespace Yara.AppService.Dtos
{
    public class ContaClienteParticipanteGarantiaDto : BaseDto
    {
        public string Documento { get; set; }
        public string Nome { get; set; }
        public bool Garantido { get; set; }
        public bool Ativo { get; set; }
        public Guid ContaClienteGarantiaID { get; set; }
        public ContaClienteGarantiaDto ContaClienteGarantia { get; set; }
    }
}
