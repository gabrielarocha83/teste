using System;

namespace Yara.AppService.Dtos
{
    public class ContaClienteRepresentanteDto
    {
        public Guid ContaClienteID { get; set; }
        public Guid RepresentanteID { get; set; }
        public string CodigoSapCTC { get; set; }
        public string EmpresasID { get; set; }
        public DateTime DataCriacao { get; set; }

        public ContaClienteDto ContaCliente { get; set; }
        public RepresentanteDto Representante { get; set; }
        public EmpresasDto Empresas { get; set; }
    }
}
