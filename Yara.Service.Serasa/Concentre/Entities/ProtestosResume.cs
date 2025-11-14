using System;
using System.Collections.Generic;

namespace Yara.Service.Serasa.Concentre.Entities
{
    public class ProtestosResume
    {
        public DateTime DataInicial { get; set; }
        public DateTime DataFinal { get; set; }
        public int QtdeTotal { get; set; }
        public decimal Valor { get; set; }
        public string Origem { get; set; }
        public List<ProtestosDetail> ProtestosDetail { get; set; }
        public List<ProtestosSubJudice> ProtestosSubJudice { get; set; }

        public ProtestosResume()
        {
            ProtestosDetail = new List<ProtestosDetail>();
            ProtestosSubJudice = new List<ProtestosSubJudice>();
        }
    }
}
