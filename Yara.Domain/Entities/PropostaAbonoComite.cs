using System;

namespace Yara.Domain.Entities
{
    public class PropostaAbonoComite
    {
        public Guid ID { get; set; }

        public Guid PropostaAbonoID { get; set; }
        public virtual PropostaAbono PropostaAbono { get; set; }

        public Guid PropostaAbonoComiteSolicitanteID { get; set; }
        public virtual PropostaAbonoComiteSolicitante PropostaAbonoComiteSolicitante { get; set; }

        public DateTime DataCriacao { get; set; }

        public int Nivel { get; set; }

        public int Round { get; set; }

        public virtual Perfil Perfil { get; set; }
        public Guid PerfilID { get; set; }

        public virtual Usuario Usuario { get; set; }
        public Guid UsuarioID { get; set; }

        public DateTime? DataAcao { get; set; }

        public decimal ValorDe { get; set; }
        public decimal ValorAte { get; set; }

        // Campos específicos
        public bool ConceitoH { get; set; }

        public bool Aprovado { get; set; }
        public string Comentario { get; set; }
        public bool Ativo { get; set; }

        public Guid FluxoLiberacaoAbonoID { get; set; }
        public virtual FluxoLiberacaoAbono FluxoLiberacaoAbono { get; set; }
    }
}