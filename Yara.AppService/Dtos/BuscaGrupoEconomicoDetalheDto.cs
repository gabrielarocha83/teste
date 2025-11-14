using System;
using System.Collections.Generic;

namespace Yara.AppService.Dtos
{
    public class BuscaGrupoEconomicoDetalheDto
    {
        public Guid GrupoId { get; set; }
        public string GrupoNome { get; set; }
        public decimal LcGrupo { get; set; }
        public decimal ExpGrupo { get; set; }
        public string StatusGrupo { get; set; }
        public string ClassificacaoNome { get; set; }
        public List<BuscaGrupoEconomicoMembrosDetalheDto> MembrosDetalhes { get; set; }

        public BuscaGrupoEconomicoDetalheDto()
        {
            MembrosDetalhes = new List<BuscaGrupoEconomicoMembrosDetalheDto>();
        }
    }
}
