using System.Collections.Generic;

namespace Yara.Domain.Entities
{
    public class TipoEndividamento : Base
    {
        public string Tipo { get; set; }

        public bool Ativo { get; set; }

        public ICollection<PropostaLCTipoEndividamento> PropostaLCTiposEndividamentos { get; set; }
    }
}
