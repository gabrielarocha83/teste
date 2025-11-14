using System;

namespace Yara.Service.Serasa.Crednet.Entities
{
    public class ProtestoEstadual
    {
        public DateTime? DataOcorrencia { get; set; }
        public string TipoMoeda { get; set; }
        public decimal Valor { get; set; }
        public string Cartorio { get; set; }
        public string Origem { get; set; }
        public string Sigla { get; set; }

        public decimal Total => Valor;
    }
}
