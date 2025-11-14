namespace Yara.Domain.Entities
{
    public class ConceitoCobranca:Base
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public bool Ativo { get; set; }
    }
}