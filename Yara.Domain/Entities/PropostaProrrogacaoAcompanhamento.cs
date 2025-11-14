using System;

namespace Yara.Domain.Entities
{
    public class PropostaProrrogacaoAcompanhamento
    {
        public PropostaProrrogacao PropostaProrrogacao { get; set; }
        public Guid PropostaProrrogacaoID { get; set; }
        public Usuario Usuario { get; set; }
        public Guid UsuarioID { get; set; }
        public DateTime DataCriacao { get; set; }
        public bool Ativo { get; set; }
    }
}
