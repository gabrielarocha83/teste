using System;
using System.Collections.Generic;

namespace Yara.Domain.Entities
{
    public class TipoGarantia:Base
    {
        public TipoGarantia()
        {
            
        }

        public TipoGarantia(string nome, bool ativo)
        {
            Nome = nome;
            Ativo = ativo;
        }

        // Properties
        public string Nome { get; set; }
        public Boolean Ativo { get; set; }

        // Navigation Properties
        public ICollection<PropostaLCGarantia> PropostaLCGarantias { get; set; }

        public ICollection<PropostaAlcadaComercial> PropostaAlcadaComerciais { get; set; }


    }
}