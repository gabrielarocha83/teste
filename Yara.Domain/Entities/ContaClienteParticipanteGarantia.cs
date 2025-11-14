using System;

namespace Yara.Domain.Entities
{
    public class ContaClienteParticipanteGarantia : Base
    {
        public string Documento { get; set; }
        public string Nome { get; set; }
        public bool Garantido { get; set; }
        public bool Ativo { get; set; }
        public Guid ContaClienteGarantiaID { get; set; }
        public ContaClienteGarantia ContaClienteGarantia { get; set; }
    }
}
