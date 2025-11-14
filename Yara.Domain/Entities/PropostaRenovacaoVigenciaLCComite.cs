using System;

namespace Yara.Domain.Entities
{
    public class PropostaRenovacaoVigenciaLCComite
    {
        public Guid ID { get; set; }

        public Guid PropostaRenovacaoVigenciaLCID { get; set; }
        public virtual PropostaRenovacaoVigenciaLC PropostaRenovacaoVigenciaLC { get; set; }

        public DateTime DataCriacao { get; set; }

        public int Nivel { get; set; }

        public virtual Usuario Usuario { get; set; }
        public Guid UsuarioID { get; set; }

        public DateTime? DataAcao { get; set; }

        public string Comentario { get; set; }

        public PropostaLCStatusComite StatusComite { get; set; }
        public string StatusComiteID { get; set; }

        public Guid FluxoRenovacaoVigenciaLCID { get; set; }
        public virtual FluxoRenovacaoVigenciaLC FluxoRenovacaoVigenciaLC { get; set; }
    }
}
