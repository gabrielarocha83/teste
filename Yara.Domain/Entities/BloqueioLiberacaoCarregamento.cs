using System;

namespace Yara.Domain.Entities
{
    public class BloqueioLiberacaoCarregamento : Base
    {
        public Guid ProcessamentoCarteiraID { get; set; }
        public ProcessamentoCarteira ProcessamentoCarteira { get; set; }
        public int Divisao { get; set; }
        public int Item { get; set; }
        public string Numero { get; set; }
        public bool EnviadoSAP { get; set; }
        public bool Bloqueada { get; set; }
    }
}