using System;

namespace Yara.Domain.Entities
{
    public class SolicitanteFluxo : Base
    {
        public string Comentario { get; set; }
        public bool AcompanharProposta { get; set; }
        public string EmpresasId { get; set; }
        public Empresas Empresas { get; set; }
        public decimal Total { get; set; }
        public Guid ContaClienteID { get; set; }
        public int NumeroInternoProposta { get; set; }
    }
}
