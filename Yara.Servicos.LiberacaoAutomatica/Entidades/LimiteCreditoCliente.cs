namespace Yara.Servicos.LiberacaoAutomatica.Entidades
{
    public class LimiteCreditoCliente
    {
        public decimal? ValorDisponivel { get; set; }
        public decimal? ValorAdicional { get; set; }
        public decimal? ValorMargem { get; set; }

        public decimal? ValorTitulos { get; set; }
        public decimal? ValorOrdens { get; set; }

        public decimal? ValorTotalSemAdicional => (this.ValorDisponivel + this.ValorMargem) - (this.ValorTitulos + this.ValorOrdens);
        public decimal? ValorTotalComAdicional => (this.ValorDisponivel + this.ValorMargem + ValorAdicional) - (this.ValorTitulos + this.ValorOrdens);
    }
}