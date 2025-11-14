using System;

namespace Yara.Domain.Entities
{
    public class PropostaJuridicoGarantia : Base
    {
        public Guid PropostaJuridicoID { get; set; }
        public Guid TipoGarantiaID { get; set; }
        public string Nome { get; set; }

        public TipoGarantia TipoGarantia { get; set; }
        public PropostaJuridico PropostaJuridico { get; set; }
    }
}
