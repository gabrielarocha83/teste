using System;

namespace Yara.Service.Serasa.Concentre.Entities
{
    public class AcaoJudicialDetail
    {
        public DateTime DataOcorrencia { get; set; }
        public string VaraCivil { get; set; }
        public string NumeroDistribuidor { get; set; }
        public string Natureza { get; set; }
        public decimal Valor { get; set; }
        public string Praca { get; set; }
        public string Uf { get; set; }
        public string Cidade { get; set; }
        public string Principal { get; set; }
        public string SubJudice { get; set; }
        public int CnpjFilial { get; set; }
        public int DigitoDocumento { get; set; }
        public DateTime DataInclusao { get; set; }
        public string HoraInclusao { get; set; }
        public string ChaveCadus { get; set; }
        public decimal Total => Valor;
        public string TotalOcorrencia { get; set; }
    }
}
