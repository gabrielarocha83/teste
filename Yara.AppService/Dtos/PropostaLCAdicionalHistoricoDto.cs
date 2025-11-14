using System;

namespace Yara.AppService.Dtos
{
    public class PropostaLCAdicionalHistoricoDto
    {
        public Guid ID { get; set; }
        public DateTime DataCriacao { get; set; }
        public Guid PropostaLCAdicionalID { get; set; }
        public string PropostaLCStatusID { get; set; }
        public Guid UsuarioID { get; set; }
        public string Descricao { get; set; }

        public virtual PropostaLCAdicionalDto PropostaLCAdicional { get; set; }
        public virtual PropostaLCStatusDto PropostaLCStatus { get; set; }
        public virtual UsuarioDto Usuario { get; set; }
    }
}
