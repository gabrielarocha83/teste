using System;

namespace Yara.Domain.Entities
{
    public class ContaClienteBuscaBens : Base
    {
        public Guid ContaClienteID { get; set; }
        public DateTime DataSolicitacao { get; set; }
        public DateTime? DataPatrimonio { get; set; }
        public string Patrimonio { get; set; }
        public string EmpresasID { get; set; }

        public Empresas Empresas { get; set; }
        public ContaCliente ContaCliente { get; set; }
    }
}
