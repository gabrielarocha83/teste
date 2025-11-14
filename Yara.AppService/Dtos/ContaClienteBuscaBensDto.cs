using System;

namespace Yara.AppService.Dtos
{
    public class ContaClienteBuscaBensDto : BaseDto
    {
        public Guid ContaClienteID { get; set; }
        public DateTime DataSolicitacao { get; set; }
        public DateTime? DataPatrimonio { get; set; }
        public string Patrimonio { get; set; }
        public string EmpresasID { get; set; }

        public EmpresasDto Empresas { get; set; }
        public ContaClienteDto ContaCliente { get; set; }
    }
}
