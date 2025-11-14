using System;


namespace Yara.AppService.Dtos
{
    public class LogDivisaoRemessaLiberacaoDto : BaseDto
    {
        public Guid ProcessamentoCarteiraID { get; set; }
        public virtual ProcessamentoCarteiraDto ProcessamentoCarteira { get; set; }
        public int OrdemDivisao { get; set; }
        public string OrdemVendaNumero { get; set; }
        public int OrdemVendaItem { get; set; }
        public string Restricao { get; set; }
    }
}
