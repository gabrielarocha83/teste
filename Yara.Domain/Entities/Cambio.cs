using System;
using System.Collections.Generic;

namespace Yara.Domain.Entities
{
    public class Cambio
    {

        public DateTime InicioValidade { get; set; }
        public string MoedaDe { get; set; }
        public string MoedaPara { get; set; }
        public int FatorDe { get; set; }
        public int FatorPara { get; set; }
        public decimal Taxa { get; set; }

    }
}