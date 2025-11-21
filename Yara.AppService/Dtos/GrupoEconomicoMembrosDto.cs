using System;

namespace Yara.AppService.Dtos
{
    public class GrupoEconomicoMembrosDto
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

        public Guid ContaClienteIDAcesso { get; set; }
        public string ContaClienteNome { get; set; }
        public string GrupoEconomicoNome { get; set; }
        public string EmpresaID { get; set; }

        public bool ExplodeGrupo { get; set; }

        public StatusGrupoEconomicoFluxoDto StatusGrupoEconomicoFluxoDto { get; set; }
        public virtual ContaClienteDto ContaCliente { get; set; }
        public virtual GrupoEconomicoDto GrupoEconomico { get; set; }
    }

    public class GrupoEconomicoMembrosFluxoDto
    {
    }
}
