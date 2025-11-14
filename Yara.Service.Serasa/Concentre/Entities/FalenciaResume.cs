using System;
using System.Collections.Generic;

namespace Yara.Service.Serasa.Concentre.Entities
{
    public class FalenciaResume
    {
        public DateTime DataInicial { get; set; }
        public DateTime DataFinal { get; set; }
        public int QtdeTotal { get; set; }
        public decimal Valor { get; set; }
        public string Origem { get; set; }
        public List<FalenciaDetail> FalenciaDetail { get; set; }

        public FalenciaResume()
        {
            FalenciaDetail = new List<FalenciaDetail>();
        }
    }
}
