using Yara.AppService.Dtos;

namespace Yara.AppService.Dtos
{
    public class StatusOrdemVendasDto : BaseDto
    {
        public string Status { get; set; }
        public string Descricao { get; set; }
    }
}
