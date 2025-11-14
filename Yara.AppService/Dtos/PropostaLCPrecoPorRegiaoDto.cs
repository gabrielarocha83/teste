using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yara.AppService.Dtos
{
    public class PropostaLCPrecoPorRegiaoDto
    {

        // Properties
        public Guid ID { get; set; }
        public Guid PropostaLCID { get; set; }
        public Guid? CidadeID { get; set; }
        public string Documento { get; set; }

        public decimal? ValorHaCultivavel { get; set; }
        public decimal? ValorHaNaoCultivavel { get; set; }
        public decimal? ModuloRural { get; set; }

        public decimal? ValorHaCultivavelParametro { get; set; }
        public decimal? ValorHaNaoCultivavelParametro { get; set; }
        public decimal? ModuloRuralParametro { get; set; }

        // Navigation Properties
        [JsonIgnore]
        public PropostaLCDto PropostaLC { get; set; }
        public virtual CidadeDto Cidade { get; set; }


    }
}
