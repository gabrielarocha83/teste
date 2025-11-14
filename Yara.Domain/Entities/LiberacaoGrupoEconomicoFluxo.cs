using System;

namespace Yara.Domain.Entities
{
    public class LiberacaoGrupoEconomicoFluxo : Base
    {
        public Guid SolicitanteGrupoEconomicoID { get; set; }
        public SolicitanteGrupoEconomico SolicitanteGrupoEconomico { get; set; }

        public Guid FluxoGrupoEconomicoID { get; set; }
        public FluxoGrupoEconomico FluxoGrupoEconomico { get; set; }

        public string CodigoSap { get; set; }
        public EstruturaComercial EstruturaComercial { get; set; }

        public Guid? UsuarioID { get; set; }
        public virtual Usuario Usuario { get; set; }

        public string StatusGrupoEconomicoFluxoID { get; set; }
        public StatusGrupoEconomicoFluxo StatusGrupoEconomicoFluxo { get; set; }

        public Guid GrupoEconomicoID { get; set; }
        public virtual GrupoEconomico GrupoEconomico { get; set; }
    }
}