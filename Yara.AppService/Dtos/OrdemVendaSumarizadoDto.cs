namespace Yara.AppService.Dtos
{
    public class OrdemVendaSumarizadoDto
    {
        public int Tipo { get; set; }
        public decimal? Toneladas { get; set; }
        public decimal? Valor { get; set; }
        public decimal? Exposicao { get; set; }
    }
}