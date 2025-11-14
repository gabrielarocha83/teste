using System;

namespace Yara.Domain.Entities
{
    public class ConfiguracaoPerfilUsuario : Base
    {
        public string CodigoSap { get; set; }
        public Guid GrupoId { get; set; }
        public Guid UsuarioId { get; set; }

        public Grupo Grupo { get; set; }
        public Usuario Usuario { get; set; }
    }
}
