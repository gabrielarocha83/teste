using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yara.AppService.Dtos
{
    public class BuscaGrupoEconomicoDto
    {
        public Guid GrupoId { get; set; }
        public string GrupoNome { get; set; }
        public string GrupoCodigo { get; set; }
        public DateTime UltimaAtualizacao { get; set; }
        public string TipoRelacao { get; set; }
        public decimal LcTotalGrupo { get; set; }
        public decimal ExpTotalGrupo { get; set; }
        public string ClassificacaoNome { get; set; }
        public int ClassificacaoID { get; set; }
        public string StatusGrupo { get; set; }
    }
}
