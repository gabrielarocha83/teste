using System.Collections.Generic;

namespace Yara.Domain.Entities
{
    public class AreaIrrigada : Base
    {
        public string Nome { get; set; }
        public bool Ativo { get; set; }

        public ICollection<PropostaLC> PropostasLC { get; set; }

    }
}
