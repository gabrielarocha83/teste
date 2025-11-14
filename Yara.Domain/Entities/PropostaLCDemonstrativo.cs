using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yara.Domain.Entities
{
    public class PropostaLCDemonstrativo
    {

        public Guid ID { get; set; }
        public string NomeArquivo { get; set; }
        public string Html { get; set; }
        public string HtmlResumo { get; set; }
        public string HtmlRating { get; set; }
        public string Rating { get; set; }
        public decimal? PotencialCredito { get; set; }
        public string Tipo { get; set; }
        public DateTime DataUpload { get; set; }
        public byte[] Conteudo { get; set; }


    }
}
