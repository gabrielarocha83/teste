using System;

namespace Yara.AppService.Dtos
{
    public class FluxoGrupoEconomicoDto : BaseDto
    {

        public int Nivel { get; set; }
        public bool Ativo { get; set; }
        public string EmpresaID { get; set; }
        public int ClassificacaoGrupoEconomicoId { get; set; }
        public Guid PerfilId { get; set; }

        // Nav Properties
        public virtual PerfilDto Perfil { get; set; }
        public ClassificacaoGrupoEconomicoDto ClassificacaoGrupoEconomico { get; set; }

    }
}
