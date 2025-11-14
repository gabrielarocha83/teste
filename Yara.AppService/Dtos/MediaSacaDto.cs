namespace Yara.AppService.Dtos
{
    public class MediaSacaDto : BaseDto
    {
        public string Nome { get; set; }
        public decimal Peso { get; set; }
        public decimal Valor { get; set; }
        public bool Ativo { get; set; }
    }
}
