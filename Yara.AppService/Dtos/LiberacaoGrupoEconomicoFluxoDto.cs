using System;
using Yara.AppService.Dtos;
using Yara.Domain.Entities;

namespace Yara.AppService.Dtos
{
    public class LiberacaoGrupoEconomicoFluxoDto : BaseDto
    {
        public Guid SolicitanteGrupoEconomicoID { get; set; }
        public SolicitanteGrupoEconomicoDto SolicitanteGrupoEconomico { get; set; }

        public Guid FluxoGrupoEconomicoID { get; set; }
        public FluxoGrupoEconomicoDto FluxoGrupoEconomico { get; set; }

        public string CodigoSap { get; set; }
        public EstruturaComercialDto EstruturaComercial { get; set; }

        public Guid? UsuarioID { get; set; }
        public virtual UsuarioDto Usuario { get; set; }

        public string StatusGrupoEconomicoFluxoID { get; set; }
        public StatusGrupoEconomicoFluxoDto StatusGrupoEconomicoFluxo { get; set; }

        public Guid GrupoEconomicoID { get; set; }
        public virtual GrupoEconomicoDto GrupoEconomico { get; set; }
    }
}