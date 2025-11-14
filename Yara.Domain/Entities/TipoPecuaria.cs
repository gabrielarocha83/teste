using System.Collections.Generic;
using System.Security.AccessControl;

namespace Yara.Domain.Entities
{
    public class TipoPecuaria:Base
    {
        public string Tipo { get; set; }
        public string UnidadeMedida { get; set; }
        public bool Ativo { get; set; }

        public ICollection<PropostaLCPecuaria> PropostaLCPecuarias { get; set; }

    }
}