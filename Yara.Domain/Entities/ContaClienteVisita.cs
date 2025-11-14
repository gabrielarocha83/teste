using System;

namespace Yara.Domain.Entities
{
    public class ContaClienteVisita : Base
    {
        public Guid ContaClienteID { get; set; }
        public DateTime DataSolicitacao { get; set; }
        public DateTime? DataParecer { get; set; }
        public string Parecer { get; set; }
        public string EmpresasID { get; set; }

        public Empresas Empresas { get; set; }
        public ContaCliente ContaCliente { get; set; }
    }
}
