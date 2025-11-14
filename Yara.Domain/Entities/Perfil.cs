namespace Yara.Domain.Entities
{
    public class Perfil : Base
    {
        public string Descricao { get; set; }
        public bool Ativo { get; set; }
        public int Ordem { get; set; }
    }
}
