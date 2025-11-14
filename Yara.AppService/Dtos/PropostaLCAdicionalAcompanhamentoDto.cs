using System;

namespace Yara.AppService.Dtos
{
    public class PropostaLCAdicionalAcompanhamentoDto
    {
        public Guid PropostaLCAdicionalID { get; set; }
        public Guid UsuarioIDCriacao { get; set; }
        public DateTime DataCriacao { get; set; }
        public bool Ativo { get; set; }

        public virtual UsuarioDto Usuario { get; set; }
        public virtual PropostaLCAdicionalDto PropostaLCAdicional { get; set; }
    }
}
