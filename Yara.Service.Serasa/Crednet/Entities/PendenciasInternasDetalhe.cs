using System;

namespace Yara.Service.Serasa.Crednet.Entities
{
    public class PendenciasInternasDetalhe
    {
        public DateTime? DataOcorrencia { get; set; }
        public string Modalidade { get; set; }
        public string Avalista { get; set; }
        public string TipoMoeda { get; set; }
        public decimal Valor { get; set; }
        public string Contrato { get; set; }
        public string Origem { get; set; }
        public string Sigla { get; set; }

        public decimal Total => Valor;
    }
}
