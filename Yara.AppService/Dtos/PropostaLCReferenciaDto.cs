using System;
using Newtonsoft.Json;
using Yara.AppService.Dtos;
using Yara.Domain.Entities;

namespace Yara.AppService.Dtos
{
    public class PropostaLCReferenciaDto
    {

        public Guid ID { get; set; }
        public Guid PropostaLCID { get; set; }
        public string TipoReferencia { get; set; }
        public Guid? TipoEmpresaID { get; set; }
        public string NomeEmpresa { get; set; }
        public string NomeBanco { get; set; }
        public string Municipio { get; set; }
        public string Telefone { get; set; }
        public string NomeContato { get; set; }
        public string Desde { get; set; }
        public decimal? LCAtual { get; set; }
        public DateTime? Vigencia { get; set; }
        public string Garantias { get; set; }
        public string Comentarios { get; set; }

        [JsonIgnore]
        public PropostaLCDto PropostaLC { get; set; }

        public virtual TipoEmpresaDto TipoEmpresa { get; set; }

    }
}