namespace Yara.Domain.Entities.Procedures
{
    public class DadosFinanceiroTitulo
    {
        public decimal? TitulosVencer { get; set; }
        public decimal? TitulosVencido { get; set; }
        public decimal? TitulosOrdemVendaDisponivel { get; set; }
        public decimal? TitulosOrdemVendaAplicado { get; set; }
    }
}
