using System;

namespace Yara.AppService.Dtos
{
    public class ConceitoManualContaClienteDto
    {
        public Guid ContaClienteID { get; set; }
        public Guid ConceitoCobrancaID { get; set; }
        public Guid UsuarioID { get; set; }
        public string EmpresasID { get; set; }
        public string LogMessage { get; set; }
    }
}
