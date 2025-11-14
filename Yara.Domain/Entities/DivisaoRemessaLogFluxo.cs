using System;

namespace Yara.Domain.Entities
{
    public class DivisaoRemessaLogFluxo
    {
        public int Nivel { get; set; }
        public string Solicitante { get; set; }
        public string Responsavel { get; set; }
        public string Status { get; set; }
        public DateTime DataSolicitacao { get; set; }
        public DateTime? DataAcao { get; set; }
    }
}