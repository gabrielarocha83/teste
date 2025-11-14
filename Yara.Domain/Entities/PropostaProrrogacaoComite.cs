using System;

namespace Yara.Domain.Entities
{
    public class PropostaProrrogacaoComite
    {
        public Guid ID { get; set; }

        public Guid PropostaProrrogacaoID { get; set; }
        public virtual PropostaProrrogacao PropostaProrrogacao { get; set; }

        public Guid PropostaProrrogacaoComiteSolicitanteID { get; set; }
        public virtual PropostaProrrogacaoComiteSolicitante PropostaProrrogacaoComiteSolicitante { get; set; }

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
        public bool NovasLiberacoes { get; set; }
        public float TaxaJuros { get; set; }

        public bool Aprovado { get; set; }
        public string Comentario { get; set; }
        public bool Ativo { get; set; }

        public Guid FluxoLiberacaoProrrogacaoID { get; set; }
        public virtual FluxoLiberacaoProrrogacao FluxoLiberacaoProrrogacao { get; set; }
    }
}