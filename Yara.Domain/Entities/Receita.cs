using System.Collections.Generic;

namespace Yara.Domain.Entities
{
    public class Receita : Base
    {

        public string Descricao { get; set; }
        public bool Ativo { get; set; }

        public ICollection<PropostaLCOutraReceita> PropostaLCOutrasReceitas { get; set; }

    }
}
