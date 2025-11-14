namespace Yara.AppService.Dtos
{
    public class ControleCobrancaEnvioJuridicoDto
    {
        public string Mes { get; set; }
        public string Ano { get; set; }
        public int? Quantidade { get; set; }
        public decimal? Valor { get; set; }
    }
}
