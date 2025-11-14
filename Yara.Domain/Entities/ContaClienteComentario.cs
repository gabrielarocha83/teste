using System;

namespace Yara.Domain.Entities
{
    public class ContaClienteComentario : Base
    {
        public Guid ContaClienteID { get; set; }
        public Guid UsuarioID { get; set; }
        public string Descricao { get; set; }
        public bool Ativo { get; set; }

        public virtual ContaCliente ContaCliente { get; set; }
        public virtual Usuario Usuario { get; set; }
        
    }
}
