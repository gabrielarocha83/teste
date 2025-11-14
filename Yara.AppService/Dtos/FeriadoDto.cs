using System;

namespace Yara.AppService.Dtos
{
    public class FeriadoDto : BaseDto
    {
        public DateTime DataFeriado { get; set; }
        public string Descricao { get; set; }
        public bool Ativo { get; set; }
    }
}
