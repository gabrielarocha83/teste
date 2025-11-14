using System;

namespace Yara.AppService.Dtos
{
    public class ConceitoCobrancaLiberacaoLogDto
    {
        public Guid ContaClienteId { get; set; }
        public bool Status { get; set; }
        public string EmpresaID { get; set; }
        public string ComentarioStatus { get; set; }
        public Guid UsuarioID { get; set; }
    }
}
