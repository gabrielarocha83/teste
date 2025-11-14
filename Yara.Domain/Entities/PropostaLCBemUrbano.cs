using System;

namespace Yara.Domain.Entities
{
    public class PropostaLCBemUrbano
    {

        // Properties
        public Guid ID { get; set; }
        public Guid PropostaLCID { get; set; }
        public Guid? GarantiaID { get; set; }
        public string IR { get; set; }
        public string Descricao { get; set; }
        public decimal? AreaTotal { get; set; }
        public decimal? ValorComBenfeitorias { get; set; }
        public decimal? Onus { get; set; }
        public decimal? ValorAvaliado { get; set; }
        public string Documento { get; set; }
        // Navigation Properties
        public PropostaLC PropostaLC { get; set; }
        public virtual PropostaLCGarantia PropostaLCGarantia { get; set; }


    }
}
