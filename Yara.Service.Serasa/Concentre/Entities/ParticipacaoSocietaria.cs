using System;

namespace Yara.Service.Serasa.Concentre.Entities
{
    public class ParticipacaoSocietaria
    {
        public string NomeEmpresa { get; set; }
        public string CnpjEmpresa { get; set; }
        public decimal NivelParticipacao { get; set; }
        public DateTime DataDesde { get; set; }
        public DateTime DataAtual { get; set; }
        public string Uf { get; set; }
        public string Mensagem { get; set; }
        public bool RestricaoParticipante { get; set; }
    }
}
