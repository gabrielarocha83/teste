using System;

namespace Yara.AppService.Dtos
{
    public class PropostaLCTipoEndividamentoDto
    {

        public Guid ID { get; set; }
        public Guid? PropostaLCID { get; set; }
        public Guid? TipoEndividamentoID { get; set; }
        public string Documento { get; set; }
        public decimal? CurtoPrazo { get; set; }
        public decimal? LongoPrazo { get; set; }
        
        public PropostaLCDto PropostaLC { get; set; }
        public virtual TipoEndividamentoDto TipoEndividamento { get; set; }

    }
}