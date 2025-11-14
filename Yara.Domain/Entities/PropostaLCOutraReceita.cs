using System;

namespace Yara.Domain.Entities
{
    public class PropostaLCOutraReceita
    {

        public Guid ID { get; set; }
        public Guid? PropostaLCID { get; set; }
        public Guid? ReceitaID { get; set; }
        public string Documento { get; set; }
        public int? AnoOutrasReceitas { get; set; }
        public decimal? ReceitaPrevista { get; set; }
        
        public PropostaLC PropostaLC { get; set; }
        public virtual Receita Receita { get; set; }

    }
}