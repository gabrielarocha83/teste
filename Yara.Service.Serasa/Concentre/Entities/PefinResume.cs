using System;
using System.Collections.Generic;

namespace Yara.Service.Serasa.Concentre.Entities
{
    public class PefinResume
    {
        public DateTime DataInicialPefin { get; set; }
        public DateTime DataFinalPefin { get; set; }
        public int QtdTotal { get; set; }
        public decimal ValorPefin { get; set; }
        public string TipoOcorrencia { get; set; }
        public string NomeCredor { get; set; }
        public List<PefinDetail> PefinDetail{ get; set; }
        public List<PefinDetail2> PefinDetail2 { get; set; }

        public PefinResume()
        {
            PefinDetail = new List<PefinDetail>();
            PefinDetail2 = new List<PefinDetail2>();
        }
    }
}
