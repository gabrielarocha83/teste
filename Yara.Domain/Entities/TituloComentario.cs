using System;

namespace Yara.Domain.Entities
{
    public class TituloComentario : Base
    {
        public string NumeroDocumento { get; set; }
        public Guid UsuarioID { get; set; }
        public virtual Usuario Usuario { get; set; }
        public string Linha { get; set; }
        public string AnoExercicio { get; set; }
        public string Empresa { get; set; }
        public string Texto{ get; set; }
    }
}
