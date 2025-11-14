using System;

namespace Yara.Domain.Entities
{
    public class ContaClienteCodigo : Base
    {
        public Guid ContaClienteID { get; set; }
        public bool CodigoPrincipal { get; set; }
        public string Codigo { get; set; }
        public string Documento { get; set; }

        public ContaCliente ContaCliente { get; set; }
    }
}
