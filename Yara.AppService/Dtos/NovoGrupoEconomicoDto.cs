using System;
using System.Collections.Generic;

namespace Yara.AppService.Dtos
{
    public class NovoGrupoEconomicoDto
    {
        public Guid ID { get; set; }
        public string Nome { get; set; }
        public Guid ContaClientePrincipalID { get; set; }
        public Guid TipoRelacaoGrupoEconomico { get; set; }
        public int ClassificacaoGrupoEconomicoID { get; set; }
        public bool Ativo { get; set; }
        public Guid UsuarioIDCriacao { get; set; }
        public string EmpresaID { get; set; }
        public List<ContaClienteDto> Membros { get; set; }
    }

    public class GrupoEconomicoFluxoDto
    {
        public Guid GrupoID { get; set; }
        public string Nome { get; set; }
        public Guid ContaClienteID { get; set; }
        public int ClassificacaoGrupoEconomicoID { get; set; }
        public Guid UsuarioID { get; set; }
        public string EmpresaID { get; set; }
    }
}