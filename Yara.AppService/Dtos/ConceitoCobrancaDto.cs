namespace Yara.AppService.Dtos
{
    public class ConceitoCobrancaDto : BaseDto
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public bool Ativo { get; set; }
    }
}