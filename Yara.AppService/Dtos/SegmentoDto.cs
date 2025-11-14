namespace Yara.AppService.Dtos
{
    public class SegmentoDto : BaseDto
    {
        public string Descricao { get; set; }
        public bool Ativo { get; set; }
       // [JsonIgnore]
       // public virtual ICollection<ContaClienteDto> ContaClientes { get; set; }
    }
}
