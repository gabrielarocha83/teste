using System;

namespace Yara.Domain.Entities
{
    public class PropostaLCComite
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

        public Guid PropostaLCID { get; set; }
        public Guid PerfilID { get; set; }
        public Guid UsuarioID { get; set; }
        public Guid PropostaLcComiteSolicitanteID { get; set; }
        public Guid FluxoLiberacaoLimiteCreditoID { get; set; }
        public string StatusComiteID { get; set; }

        public virtual PropostaLC PropostaLC { get; set; }
        public virtual Perfil Perfil { get; set; }
        public virtual Usuario Usuario { get; set; }
        public virtual PropostaLCComiteSolicitante PropostaLcComiteSolicitante { get; set; }
        public virtual FluxoLiberacaoLimiteCredito FluxoLiberacaoLimiteCredito { get; set; }
        public PropostaLCStatusComite StatusComite { get; set; }

    }

    public class PropostaLCComiteSolicitante
    {
        public Guid ID { get; set; }
        public Usuario Usuario { get; set; }
        public Guid UsuarioID { get; set; }
        public DateTime DataCriacao { get; set; }
    }

    public class PropostaLCStatusComite
    {
        public string ID { get; set; }
        public string Nome { get; set; }
        public bool Ativo { get; set; }
    }
}
