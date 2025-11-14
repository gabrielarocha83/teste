namespace Yara.Domain.Entities
{
    public class MediaSaca : Base
    {
        public string Nome { get; set; }
        public decimal Peso { get; set; }
        public decimal Valor { get; set; }
        public bool Ativo { get; set; }
    }
}
