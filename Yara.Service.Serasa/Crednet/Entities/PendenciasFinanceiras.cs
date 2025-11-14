using System;

namespace Yara.Service.Serasa.Crednet.Entities
{
    public class PendenciasFinanceiras
    {
        public DateTime? DataOcorrencia { get; set; }
        public string Modalidade { get; set; }
        public string Avalista { get; set; }
        public string TipoMoeda { get; set; }
        public decimal Valor { get; set; }
        public string Contrato { get; set; }
        public string Origem { get; set; }
        public string Sigla { get; set; }
        public string SubJudice { get; set; }
        public string MensagemSubJudice { get; set; }
        public string TipoAnotacao { get; set; }
        public string CodigoCadus { get; set; }
        public DateTime? DataInclusao { get; set; }
        public DateTime? DataDisponivel { get; set; }
        public string SerieCadus { get; set; }

        public decimal Total => Valor;
    }
}
