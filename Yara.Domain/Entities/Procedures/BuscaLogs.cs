using System;

namespace Yara.Domain.Entities.Procedures
{
    public class BuscaLogs
    {
        public int LogLevel { get; set; }
        public string Usuario { get; set; }
        public DateTime? DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
        public Guid? IDTransacao { get; set; }
    }
}
