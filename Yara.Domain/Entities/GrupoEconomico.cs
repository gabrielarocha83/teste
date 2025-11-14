using System;
// ReSharper disable InconsistentNaming

namespace Yara.Domain.Entities
{
    public class GrupoEconomico : Base
    {
        public string CodigoGrupo { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }

        public Guid TipoRelacaoGrupoEconomicoID { get; set; }
        public virtual TipoRelacaoGrupoEconomico TipoRelacaoGrupoEconomico { get; set; }

        public string StatusGrupoEconomicoFluxoID { get; set; }
        public virtual StatusGrupoEconomicoFluxo StatusGrupoEconomicoFluxo { get; set; }

        public int ClassificacaoGrupoEconomicoID { get; set; }
        public virtual ClassificacaoGrupoEconomico ClassificacaoGrupoEconomico { get; set; }

        public string EmpresasID { get; set; }
        public virtual Empresas Empresas { get; set; }

        public bool Ativo { get; set; }
    }
}
