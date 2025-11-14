using System;

namespace Yara.Domain.Entities
{
    public class PropostaLCCultura
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
        public Meses? MesPlantio { get; set; }
        public Meses? MesColheita { get; set; }
        public decimal? Quebra { get; set; }
        public decimal? CustoArrendamentoSacaHa { get; set; }
        public decimal? PorcentagemFertilizanteCusto { get; set; }

        public decimal? MediaFertilizantePadrao { get; set; }
        public decimal? PorcentagemFertilizanteCustoPadrao { get; set; }
        public decimal? PrecoPadrao { get; set; }
        public decimal? ProdutividadeMediaPadrao { get; set; }
        public decimal? CustoPadrao { get; set; }

        // Navigation Properties
        public PropostaLC PropostaLC { get; set; }
        public virtual Cultura Cultura { get; set; }
        public virtual Cidade Cidade { get; set; }
    }
}