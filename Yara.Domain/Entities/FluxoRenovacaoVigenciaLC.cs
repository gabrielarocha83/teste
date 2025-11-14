using System;

namespace Yara.Domain.Entities
{
    public class FluxoRenovacaoVigenciaLC : Base
    {

        public int Nivel { get; set; }
        public bool Ativo { get; set; }
        public string EmpresaID { get; set; }
        public Guid UsuarioId { get; set; }

        // Nav Properties
        public virtual Usuario Usuario { get; set; }

    }
}