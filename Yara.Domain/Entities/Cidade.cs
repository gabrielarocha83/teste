using System;
using System.Collections.Generic;

namespace Yara.Domain.Entities
{
    public class Cidade
    {
        public Guid ID { get; set; }
        public string Nome { get; set; }
        public Guid EstadoID { get; set; }
        public virtual Estado Estado { get; set; }

        public ICollection<PropostaLCBemRural> PropostaLCBensRurais { get; set; }
        public ICollection<PropostaLCPrecoPorRegiao> PropostaLCPrecosPorRegiao { get; set; }

    }
}