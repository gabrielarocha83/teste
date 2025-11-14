using System;

namespace Yara.Domain.Entities
{
    public class PropostaProrrogacaoParcelamento
    {
        public Guid ID { get; set; }
        public Guid PropostaProrrogacaoID { get; set; }
        public virtual PropostaProrrogacao PropostaProrrogacao { get; set; }
        public int Parcela { get; set; }
        public decimal Valor { get; set; }
        public bool Ativo { get; set; }
    }
}