using System;

namespace Yara.AppService.Dtos
{
    public class BloqueioManualContaClienteDto
    {
        public Guid ID { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public bool BloqueioManual { get; set; }
        public Guid UsuarioIDAlteracao { get; set; }
        public string EmpresaID { get; set; }
    }
}
