using System;
using System.Collections.Generic;

namespace Yara.Service.Serasa.Concentre.Entities
{
    public class RefinResume
    {
        public DateTime DataInicialRefin { get; set; }
        public DateTime DataFinalRefin { get; set; }
        public int QtdTotal { get; set; }
        public decimal ValorRefin { get; set; }
        public string TipoOcorrencia { get; set; }
        public string NomeCredor { get; set; }
        public List<RefinDetail> RefinDetail { get; set; }
        public List<RefinDetail2> RefinDetail2 { get; set; }
        public List<RefinDetailSubJudice> RefinDetailSubJudice { get; set; }

        public RefinResume()
        {
            RefinDetail = new List<RefinDetail>();
            RefinDetail2 = new List<RefinDetail2>();
            RefinDetailSubJudice = new List<RefinDetailSubJudice>();
        }
    }
}
