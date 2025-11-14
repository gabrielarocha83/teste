using System;

namespace Yara.Domain.Entities
{
    public class Blog
    {
        public Guid ID { get; set; }
        public Guid Area { get; set; }
        public virtual Usuario Para { get; set; }
        public Guid? ParaID { get; set; }
        public string Mensagem { get; set; }
        public virtual Usuario UsuarioCriacao { get; set; }
        public Guid UsuarioCriacaoID { get; set; }
        public DateTime DataCriacao { get; set; }

        public Empresas Empresa { get; set; }
        public string EmpresaID { get; set; }
    }
}