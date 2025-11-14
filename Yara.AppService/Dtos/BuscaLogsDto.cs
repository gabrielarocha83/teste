using System;

namespace Yara.AppService.Dtos
{
    public class BuscaLogsDto
    {
        public int LogLevel { get; set; }
        public string Usuario { get; set; }
        public DateTime? DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
        public Guid? IDTransacao { get; set; }
    }
}
