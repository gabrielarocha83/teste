using System;

namespace Yara.Service.Serasa.Concentre.Entities
{
    public class RefinDetail
    {
        public DateTime DataOcorrencia { get; set; }
        public string Natureza { get; set; }
        public string CnpjOrigem { get; set; }
        public decimal ValorOcorrencia { get; set; }
        public string TotalOcorrencia { get; set; }
        public string CodigoPraca { get; set; }
        public string Uf { get; set; }
        public string NomeEmpresa { get; set; }
        public string Cidade { get; set; }
        public string Principal { get; set; }
        public string SerieCadus { get; set; }
        public string ChaveCadus { get; set; }
    }
}
