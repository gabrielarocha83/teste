namespace Yara.Domain.Entities
{
    public class LimiteCreditoClienteDto
    {
        public decimal? ValorDisponivel { get; set; }
        public decimal? ValorAdicional { get; set; }
        public decimal? ValorMargem { get; set; }

        public decimal? ValorTitulos { get; set; }
        public decimal? ValorOrdens { get; set; }

        public decimal? ValorTotalSemAdicional { get; set; }
       
        public decimal? ValorTotalComAdicional { get; set; }
    }
}