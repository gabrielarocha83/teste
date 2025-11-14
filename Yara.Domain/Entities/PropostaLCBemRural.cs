using System;

namespace Yara.Domain.Entities
{
    public class PropostaLCBemRural
    {

        // Properties
        public Guid ID { get; set; }
        public Guid PropostaLCID { get; set; }
        public Guid? GarantiaID { get; set; }
        public string IR { get; set; }
        public Guid? CidadeID { get; set; }
        public decimal? AreaTotalHa { get; set; }
        public decimal? Benfeitorias { get; set; }
        public decimal? Onus { get; set; }

        public string Documento { get; set; }
        // Navigation Properties
        public PropostaLC PropostaLC { get; set; }
        public virtual PropostaLCGarantia PropostaLCGarantia { get; set; }
        public virtual Cidade Cidade { get; set; }


    }
}
