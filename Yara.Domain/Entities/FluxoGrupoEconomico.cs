using System;

namespace Yara.Domain.Entities
{
    public class FluxoGrupoEconomico : Base
    {

        public int Nivel { get; set; }
        public bool Ativo { get; set; }
        public string EmpresaID { get; set; }
        public int ClassificacaoGrupoEconomicoId { get; set; }
        public Guid PerfilId { get; set; }

        // Nav Properties
        public virtual Perfil Perfil { get; set; }
        public ClassificacaoGrupoEconomico ClassificacaoGrupoEconomico { get; set; }

    }
}
