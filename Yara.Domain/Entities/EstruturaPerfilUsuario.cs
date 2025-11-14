using System;

namespace Yara.Domain.Entities
{
    public class EstruturaPerfilUsuario : Base
    {
        public string CodigoSap { get; set; }
        public Guid PerfilId { get; set; }
        public Guid? UsuarioId { get; set; }

        public virtual EstruturaComercial EstruturaComercial { get; set; }
        public virtual Perfil Perfil { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
