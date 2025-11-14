namespace Yara.Domain.Entities
{
    public class TipoRelacaoGrupoEconomico:Base
    {
        public string Nome { get; set; }
        public int ClassificacaoGrupoEconomicoID { get; set; }
        public ClassificacaoGrupoEconomico ClassificacaoGrupoEconomico { get; set; }
        public bool Ativo { get; set; }
    }
}