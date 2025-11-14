using System;

namespace Yara.Service.Serasa.Concentre.Entities
{
    public class AcaoJudicialSubJudice
    {
        public string Praca { get; set; }
        public int Distribuidor { get; set; }
        public string Vara { get; set; }
        public DateTime Data { get; set; }
        public string Processo { get; set; }
        public string Mensagem { get; set; }

    }
}
