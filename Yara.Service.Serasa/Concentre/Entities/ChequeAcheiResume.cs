using System;
using System.Collections.Generic;

namespace Yara.Service.Serasa.Concentre.Entities
{
    public class ChequeAcheiResume
    {
        public DateTime DataInicialCheque { get; set; }
        public DateTime DataFinalCheque { get; set; }
        public decimal Valor { get; set; }
        public int QtdeTotal { get; set; }
        public string Origem { get; set; }
        public List<ChequeAcheiDetail> ChequeAcheiDetail { get; set; }
        public List<ChequeAcheiDetail2> ChequeAcheiDetail2 { get; set; }

        public List<ChequeSemFundoDetail> ChequeSemFundoDetail { get; set; }
        public List<ChequeSemFundoDetail2> ChequeSemFundoDetail2 { get; set; }

        public ChequeAcheiResume()
        {
            ChequeAcheiDetail = new List<ChequeAcheiDetail>();
            ChequeAcheiDetail2 = new List<ChequeAcheiDetail2>();

            ChequeSemFundoDetail = new List<ChequeSemFundoDetail>();
            ChequeSemFundoDetail2 = new List<ChequeSemFundoDetail2>();
        }
    }
}
