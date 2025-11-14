using System;

namespace Yara.Servicos.LiberacaoAutomatica.Entidades
{
    public class ProcessamentoCarteira
    {
        public Guid ID { get; set; }
        public string Cliente { get; set; }
        public DateTime DataHora { get; set; }
        public ProcessamentoCarteiraStatus Status { get; set; }
        public string Motivo { get; set; }
        public string Detalhes { get; set; }
    }
}