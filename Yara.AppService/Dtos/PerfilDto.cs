namespace Yara.AppService.Dtos
{
    public class PerfilDto : BaseDto
    {
        public string Descricao { get; set; }
        public bool Ativo { get; set; }
        public int Ordem { get; set; }
    }
}
