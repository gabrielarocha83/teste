using System;

namespace Yara.Domain.Entities
{
    public class ContaClienteRepresentante
    {
        public Guid ContaClienteID { get; set; }
        public Guid RepresentanteID { get; set; }
        public string CodigoSapCTC { get; set; }
        public string EmpresasID { get; set; }
        public DateTime DataCriacao { get; set; }

        public ContaCliente ContaCliente { get; set; }
        public virtual Representante Representante { get; set; }
        public Empresas Empresas { get; set; }
    }
}
