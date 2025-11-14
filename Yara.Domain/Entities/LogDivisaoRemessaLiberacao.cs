using System;

namespace Yara.Domain.Entities
{
    public class LogDivisaoRemessaLiberacao : Base
    {
        public Guid ProcessamentoCarteiraID { get; set; }
        public virtual ProcessamentoCarteira ProcessamentoCarteira { get; set; }
        public int OrdemDivisao { get; set; }
        public string OrdemVendaNumero { get; set; }
        public int OrdemVendaItem { get; set; }
        public string Restricao { get; set; }
    }
}
