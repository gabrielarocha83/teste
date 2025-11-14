using System;

namespace Yara.Service.Serasa.Concentre.Entities
{
    public class ChequeAcheiDetail2
    {
        public int CnpjFilial { get; set; }
        public int DigitoDocumento { get; set; }
        public DateTime DataInclusao { get; set; }
        public DateTime HoraInclusao { get; set; }
        public string ChaveCadus { get; set; }
    }
}
