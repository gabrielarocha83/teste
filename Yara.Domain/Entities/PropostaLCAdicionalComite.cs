using System;

namespace Yara.Domain.Entities
{
    public class PropostaLCAdicionalComite
    {
        public Guid ID { get; set; }
        public DateTime DataCriacao { get; set; }
        public int Nivel { get; set; }
        public int Round { get; set; }
        public DateTime? DataAcao { get; set; }
        public decimal ValorDe { get; set; }
        public decimal ValorAte { get; set; }
        public decimal? ValorEstipulado { get; set; }
        public string Comentario { get; set; }
        public bool Ativo { get; set; }
        public bool Adicionado { get; set; }
        public bool NivelFinal { get; set; }

        public Guid PropostaLCAdicionalID { get; set; }
        public Guid PerfilID { get; set; }
        public Guid UsuarioID { get; set; }
        public Guid PropostaLCAdicionalComiteSolicitanteID { get; set; }
        public Guid FluxoLiberacaoLCAdicionalID { get; set; }
        public string PropostaLCAdicionalStatusComiteID { get; set; }

        public virtual PropostaLCAdicional PropostaLCAdicional { get; set; }
        public virtual Perfil Perfil { get; set; }
        public virtual Usuario Usuario { get; set; }
        public virtual PropostaLCAdicionalComiteSolicitante PropostaLCAdicionalComiteSolicitante { get; set; }
        public virtual FluxoLiberacaoLCAdicional FluxoLiberacaoLCAdicional { get; set;}
        public virtual PropostaLCAdicionalStatusComite PropostaLCAdicionalStatusComite { get; set; }
    }

    public class PropostaLCAdicionalComiteSolicitante
    {
        public Guid ID { get; set; }
        public Usuario Usuario { get; set; }
        public Guid UsuarioID { get; set; }
        public DateTime DataCriacao { get; set; }
    }

    public class PropostaLCAdicionalStatusComite
    {
        public string ID { get; set; }
        public string Nome { get; set; }
        public bool Ativo { get; set; }
    }
}
