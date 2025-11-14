using Yara.AppService.Dtos;

namespace Yara.Domain.Entities
{
    public class TipoRelacaoGrupoEconomicoDto:BaseDto
    {
        public string Nome { get; set; }
        public int ClassificacaoGrupoEconomicoID { get; set; }
        public ClassificacaoGrupoEconomicoDto ClassificacaoGrupoEconomico { get; set; }
        public bool Ativo { get; set; }
    }
}