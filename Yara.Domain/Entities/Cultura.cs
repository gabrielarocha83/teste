using System.Collections.Generic;

namespace Yara.Domain.Entities
{
    public class Cultura : Base
    {
        public string Descricao { get; set; }
        public string UnidadeMedida { get; set; }
        public bool Ativo { get; set; }

        public ICollection<PropostaLCCultura> PropostaLCCulturas { get; set; }
        public ICollection<PropostaLCMercado> PropostaLCMercados { get; set; }
    }
}
