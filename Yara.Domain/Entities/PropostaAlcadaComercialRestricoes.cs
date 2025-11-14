using System;

namespace Yara.Domain.Entities
{
    public class PropostaAlcadaComercialRestricoes
    {
        public Guid ID { get; set; }
        public Guid ContaClienteID { get; set; }
        public string Mensagem { get; set; }
        public string EmpresasID { get; set; }

        public ContaCliente ContaCliente { get; set; }
    }
}