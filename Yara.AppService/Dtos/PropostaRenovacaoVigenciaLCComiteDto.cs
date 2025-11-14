using System;

namespace Yara.AppService.Dtos
{
    public class PropostaRenovacaoVigenciaLCComiteDto
    {
        public Guid ID { get; set; }

        public Guid PropostaRenovacaoVigenciaLCID { get; set; }
        public virtual PropostaRenovacaoVigenciaLCDto PropostaRenovacaoVigenciaLC { get; set; }

        public DateTime DataCriacao { get; set; }

        public int Nivel { get; set; }

        public virtual UsuarioDto Usuario { get; set; }
        public Guid UsuarioID { get; set; }

        public DateTime? DataAcao { get; set; }

        // Campos específicos
        public string Comentario { get; set; }

        public string EmpresaID { get; set; }

        public PropostaLCStatusComiteDto StatusComite { get; set; }
        public string StatusComiteID { get; set; }

        public Guid FluxoRenovacaoVigenciaLCID { get; set; }
        //public virtual FluxoRenovacaoVigenciaLCDto FluxoRenovacaoVigenciaLC { get; set; }
    }
}
