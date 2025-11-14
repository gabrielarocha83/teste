using System;

namespace Yara.Service.Serasa.Concentre.Entities
{
    public class ChequeSemFundoDetail
    {
        public DateTime DataOcorrencia { get; set; }
        public int Banco { get; set; }
        public int Agencia { get; set; }
        public int QtdeCheque { get; set; }
        public string Praca { get; set; }
        public string Uf { get; set; }
        public string NomeBanco { get; set; }
        public string NomeCidade { get; set; }
        public string Natureza { get; set; }
        public int CnpjFilial { get; set; }
        public int DigitoDocumento { get; set; }
        public DateTime DataInclusao { get; set; }
        public DateTime HoraInclusao { get; set; }
        public string TotalOcorrencia { get; set; }
    }
}
