using System;

namespace Yara.Domain.Entities
{
    public class PropostaLCMaquinaEquipamento
    {

        // Properties
        public Guid ID { get; set; }
        public Guid PropostaLCID { get; set; }
        public Guid? GarantiaID { get; set; }
        public string Descricao { get; set; }
        public int? Ano { get; set; }
        public decimal? Valor { get; set; }
        public string Documento { get; set; }
        // Navigation Properties
        public PropostaLC PropostaLC { get; set; }
        public virtual PropostaLCGarantia PropostaLCGarantia { get; set; }


    }
}
