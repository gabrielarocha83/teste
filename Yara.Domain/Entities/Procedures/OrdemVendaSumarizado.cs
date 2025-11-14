namespace Yara.Domain.Entities.Procedures
{
    public class OrdemVendaSumarizado
    {
        public int Tipo { get; set; }
        public decimal? Toneladas { get; set; }
        public decimal? Valor { get; set; }
        public decimal? Exposicao { get; set; }
    }
}