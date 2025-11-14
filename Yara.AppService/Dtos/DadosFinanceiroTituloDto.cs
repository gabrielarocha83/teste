namespace Yara.AppService.Dtos
{
    public class DadosFinanceiroTituloDto
    {
        public decimal? TitulosVencer { get; set; }
        public decimal? TitulosVencido { get; set; }
        public decimal? TitulosOrdemVendaDisponivel { get; set; }
        public decimal? TitulosOrdemVendaAplicado { get; set; }
    }
}
