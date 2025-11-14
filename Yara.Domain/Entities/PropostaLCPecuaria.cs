using System;

namespace Yara.Domain.Entities
{
    public class PropostaLCPecuaria
    {

        public Guid ID { get; set; }
        public Guid? PropostaLCID { get; set; }
        public Guid? TipoPecuariaID { get; set; }
        public string Documento { get; set; }
        public int? AnoPecuaria { get; set; }
        public decimal? Quantidade { get; set; }
        public decimal? Preco { get; set; }
        public decimal? Despesa { get; set; }

        public PropostaLC PropostaLC { get; set; }
        public virtual TipoPecuaria TipoPecuaria { get; set; }
    }
}