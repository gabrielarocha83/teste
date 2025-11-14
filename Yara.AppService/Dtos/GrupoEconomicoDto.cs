using System;
using Yara.Domain.Entities;

namespace Yara.AppService.Dtos
{
    public class GrupoEconomicoDto : BaseDto
    {
        public string Nome { get; set; }
        public string CodigoGrupo { get; set; }
        public string Descricao { get; set; }

        public Guid TipoRelacaoGrupoEconomicoID { get; set; }
        public virtual TipoRelacaoGrupoEconomicoDto TipoRelacaoGrupoEconomico { get; set; }

        public string StatusGrupoEconomicoFluxoID { get; set; }
        public virtual StatusGrupoEconomicoFluxoDto StatusGrupoEconomicoFluxo { get; set; }

        public int ClassificacaoGrupoEconomicoID { get; set; }
        public virtual ClassificacaoGrupoEconomicoDto ClassificacaoGrupoEconomico { get; set; }

        public string EmpresasID { get; set; }
        public virtual EmpresasDto Empresas { get; set; }

        public bool Ativo { get; set; }
    }
}