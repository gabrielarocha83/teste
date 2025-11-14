namespace Yara.AppService.Dtos
{
    public class TipoClienteDto:BaseDto
    {

        public string Nome { get; set; }
        public int LayoutProposta { get; set; }
        public TipoSerasaDto TipoSerasa { get; set; }
        public decimal Valor { get; set; }
        public bool Ativo { get; set; }
        
    }
}