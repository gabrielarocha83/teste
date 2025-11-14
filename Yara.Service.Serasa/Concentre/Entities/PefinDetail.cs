using System;

namespace Yara.Service.Serasa.Concentre.Entities
{
    public class PefinDetail
    {
        public string NomeCredor { get; set; }
        public string  DocumentoCredor { get; set; }
        public DateTime DataOcorrencia { get; set; }
        public string Natureza { get; set; }
        public decimal ValorOcorrencia { get; set; }
        public string CodigoPraca { get; set; }
        public string Principal { get; set; }
        public string Contrato { get; set; }
        public string SubJudice { get; set; }
        public string SerieCadus { get; set; }
        public string ChaveCadus { get; set; }
        public string TipoOcorrencia { get; set; }
        public string TotalOcorrencia { get; set; }
        public decimal Total => ValorOcorrencia;
    }
}
