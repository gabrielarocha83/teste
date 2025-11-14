using System;

namespace Yara.AppService.Dtos
{
    public class ContaClienteVisitaDto : BaseDto
    {
        public Guid ContaClienteID { get; set; }
        public DateTime DataSolicitacao { get; set; }
        public DateTime? DataParecer { get; set; }
        public string Parecer { get; set; }
        public string EmpresasID { get; set; }

        public EmpresasDto Empresas { get; set; }
        public ContaClienteDto ContaCliente { get; set; }
    }
}
