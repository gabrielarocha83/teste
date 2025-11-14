using System;
using System.Collections.Generic;

namespace Yara.Service.Serasa.Concentre.Entities
{
    public class AcaoJudicialResume
    {
        public DateTime DataInicial { get; set; }
        public DateTime DataFinal { get; set; }
        public int QtdeTotal { get; set; }
        public decimal Valor { get; set; }
        public string Origem { get; set; }
        public List<AcaoJudicialDetail> AcaoJudicialDetail { get; set; }
        public List<AcaoJudicialDetail2> AcaoJudicialDetail2 { get; set; }
        public List<AcaoJudicialSubJudice> AcaoJudicialSubJudice { get; set; }

        public AcaoJudicialResume()
        {
            AcaoJudicialDetail = new List<AcaoJudicialDetail>();
            AcaoJudicialDetail2 = new List<AcaoJudicialDetail2>();
            AcaoJudicialSubJudice = new List<AcaoJudicialSubJudice>();
        }
    }
}
