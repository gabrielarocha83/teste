using System;

namespace Yara.Domain.Entities
{
    public class MonitorOVResultado
    {

        public DateTime DataHora { get; set; }
        public string OrdemVenda { get; set; }
        public int Item { get; set; }
        public int Divisao { get; set; }
        public bool Liberar { get; set; }
        public bool EnviadoSAP { get; set; }
        public DateTime? DataHoraEnvio { get; set; }
        public string Restricao { get; set; }
        public string MotivoB7 { get; set; }

    }
}
