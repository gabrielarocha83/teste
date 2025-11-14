using System;

namespace Yara.Domain.Entities
{
    public class PropostaLCTipoEndividamento
    {

        public Guid ID { get; set; }
        public Guid? PropostaLCID { get; set; }
        public Guid? TipoEndividamentoID { get; set; }
        public string Documento { get; set; }
        public decimal? CurtoPrazo { get; set; }
        public decimal? LongoPrazo { get; set; }

        
        public PropostaLC PropostaLC { get; set; }
        public virtual TipoEndividamento TipoEndividamento { get; set; }

    }
}