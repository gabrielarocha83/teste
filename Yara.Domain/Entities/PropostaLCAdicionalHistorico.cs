using System;

namespace Yara.Domain.Entities
{
    public class PropostaLCAdicionalHistorico
    {
        public Guid ID { get; set; }
        public DateTime DataCriacao { get; set; }
        public Guid PropostaLCAdicionalID { get; set; }
        public string PropostaLCStatusID { get; set; }
        public Guid UsuarioID { get; set; }
        public string Descricao { get; set; }

        public virtual PropostaLCAdicional PropostaLCAdicional { get; set; }
        public virtual PropostaLCStatus PropostaLCStatus { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
