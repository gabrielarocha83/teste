namespace Yara.Domain.Entities.Procedures
{
    public class ControleCobrancaEnvioJuridico
    {
        public string Mes { get; set; }
        public string Ano { get; set; }
        public int? Quantidade { get; set; }
        public decimal? Valor { get; set; }
    }
}
