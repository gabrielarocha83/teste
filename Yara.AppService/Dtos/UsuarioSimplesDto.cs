using System;

namespace Yara.AppService.Dtos
{
    public class UsuarioSimplesDto
    {
        public Guid ID { get; set; }
        public string Nome { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string Empresa { get; set; }
        public string Acesso { get; set; }
        public int TipoAcesso { get; set; }
        public string Status { get; set; }
        public bool Ativo { get; set; }
        public string EmpresasID { get; set; }
    }
}