using System;

namespace Yara.AppService.Dtos
{
    public class BuscaGrupoEconomicoMembrosDto
    {
        public Guid GrupoId { get; set; }
        public string GrupoNome { get; set; }
        public Guid ClienteId { get; set; }
        public string ClienteNome { get; set; }
        public string ClienteCodigo { get; set; }
        public string ClienteDocumento { get; set; }
        public decimal LcIndividual { get; set; }
        public decimal ExpIndividual { get; set; }
        public decimal LcGrupo { get; set; }
        public decimal ExpGrupo { get; set; }
        public string StatusGrupo { get; set; }
        public string StatusMembro { get; set; }
    }
}
