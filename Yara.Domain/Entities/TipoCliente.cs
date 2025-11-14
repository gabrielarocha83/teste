using System.Collections.Generic;

namespace Yara.Domain.Entities
{
    public class TipoCliente : Base
    {
        public string Nome { get; set; }
        public int LayoutProposta { get; set; }
        public TipoSerasa TipoSerasa { get; set; }
        public decimal Valor { get; set; }
        public bool Ativo { get; set; }

        public virtual ICollection<PropostaLC> PropostaLCs { get; set; }
    }
}