using System;

namespace Yara.AppService.Dtos
{
    public class BlogDto
    {
        public Guid ID { get; set; }
        public Guid Area { get; set; }
        public virtual UsuarioDto Para { get; set; }
        public Guid? ParaID { get; set; }
        public string Mensagem { get; set; }
        public virtual UsuarioDto UsuarioCriacao { get; set; }
        public Guid UsuarioCriacaoID { get; set; }
        public DateTime DataCriacao { get; set; }
        public EmpresasDto Empresa { get; set; }
        public string EmpresaID { get; set; }
        public Guid ContaClienteID { get; set; }
    }
}