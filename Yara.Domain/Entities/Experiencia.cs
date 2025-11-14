using System.Collections.Generic;

namespace Yara.Domain.Entities
{
    public class Experiencia : Base
    {
        public string Descricao { get; set; }
        public bool Ativo { get; set; }

        public virtual ICollection<PropostaLC> PropostaLCs { get; set; }

    }
}
