using System;

namespace Yara.Domain.Entities
{
    public class GrupoEconomicoMembros
    {
        public Guid UsuarioIDCriacao { get; set; }
        public Guid? UsuarioIDAlteracao { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public Guid ContaClienteID { get; set; }

        public Guid GrupoEconomicoID { get; set; }
        public string StatusGrupoEconomicoFluxoID { get; set; }
        public bool MembroPrincipal { get; set; }
        public bool Ativo { get; set; }

        public decimal? LCAntesGrupo { get; set; }

        public bool ExplodeGrupo { get; set; }

        public StatusGrupoEconomicoFluxo StatusGrupoEconomicoFluxo { get; set; }
        public virtual ContaCliente ContaCliente { get; set; }
        public virtual GrupoEconomico GrupoEconomico { get; set; }
    }
}