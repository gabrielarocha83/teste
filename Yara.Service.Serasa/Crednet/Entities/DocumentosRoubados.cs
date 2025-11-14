using System;

namespace Yara.Service.Serasa.Crednet.Entities
{
    public class DocumentosRoubados
    {
        public int NumeroMensagem { get; set; }
        public int TotalMensagem { get; set; }
        public string TipoDocumento { get; set; }
        public string NumeroDocumento { get; set; }
        public string Motivo { get; set; }
        public DateTime? DataOcorrencia { get; set; }
        public int Ddd1 { get; set; }
        public long Telefone1 { get; set; }
        public int Ddd2 { get; set; }
        public long Telefone2 { get; set; }
        public int Ddd3 { get; set; }
        public long Telefone3 { get; set; }
    }
}
