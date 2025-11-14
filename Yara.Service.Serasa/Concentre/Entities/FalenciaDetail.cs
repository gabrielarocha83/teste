using System;

namespace Yara.Service.Serasa.Concentre.Entities
{
    public class FalenciaDetail
    {
        public DateTime DataOcorrencia { get; set; }
        public string Natureza { get; set; }
        public string VaraCivil { get; set; }
        public string Praca { get; set; }
        public string Uf { get; set; }
        public string Cidade { get; set; }
        public int CnpjFilial { get; set; }
        public int DigitoDocumento { get; set; }
        public DateTime DataInclusao { get; set; }
        public string HoraInclusao { get; set; }
        public string ChaveCadus { get; set; }
        public string DescricaoNatureza { get; set; }
        public string TotalOcorrencia { get; set; }
    }
}
