using System;
using Newtonsoft.Json;
using Yara.Domain.Entities;

namespace Yara.AppService.Dtos
{
    public class PropostaLCCulturaDto
    {
        public Guid ID { get; set; }
        public Guid? PropostaLCID { get; set; }
        public Guid? CulturaID { get; set; }
        public Guid? CidadeID { get; set; }
        public string Documento { get; set; }
        public decimal? Area { get; set; }
        public decimal? Arrendamento { get; set; }
        public decimal? ProdutividadeMedia { get; set; }
        public decimal? Preco { get; set; }
        public decimal? CustoHa { get; set; }
        public decimal? ConsumoFertilizante { get; set; }
        public decimal? PrecoMedioFertilizante { get; set; }
        public MesesDto? MesPlantio { get; set; }
        public MesesDto? MesColheita { get; set; }
        public decimal? Quebra { get; set; }
        public decimal? CustoArrendamentoSacaHa { get; set; }
        public decimal? PorcentagemFertilizanteCusto { get; set; }

        public decimal? MediaFertilizantePadrao { get; set; }
        public decimal? PorcentagemFertilizanteCustoPadrao { get; set; }
        public decimal? PrecoPadrao { get; set; }
        public decimal? ProdutividadeMediaPadrao { get; set; }
        public decimal? CustoPadrao { get; set; }

        // Navigation Properties
        [JsonIgnore]
        public PropostaLCDto PropostaLC { get; set; }
        public virtual CulturaDto Cultura { get; set; }
        public virtual CidadeDto Cidade { get; set; }
    }
}