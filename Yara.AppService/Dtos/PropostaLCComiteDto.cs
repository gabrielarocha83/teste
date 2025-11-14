using System;
using System.Collections.Generic;

namespace Yara.AppService.Dtos
{
    public class PropostaLCComiteDto
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

        public string CodigoSAP { get; set; }
        public string EmpresaID { get; set; }

        public ICollection<string> Aprovadores { get; set; }

        public Guid PropostaLCID { get; set; }
        public Guid PerfilID { get; set; }
        public Guid UsuarioID { get; set; }
        public Guid PropostaLcComiteSolicitanteID { get; set; }
        public Guid FluxoLiberacaoLimiteCreditoID { get; set; }
        public string StatusComiteID { get; set; }

        public virtual PropostaLCDto PropostaLC { get; set; }
        public virtual PerfilDto Perfil { get; set; }
        public virtual UsuarioDto Usuario { get; set; }
        public virtual PropostaLCComiteSolicitanteDto PropostaLcComiteSolicitante { get; set; }
        public virtual FluxoLiberacaoLimiteCreditoDto FluxoLiberacaoLimiteCredito { get; set; }
        public PropostaLCStatusComiteDto StatusComite { get; set; }
    }

    public class PropostaLCComiteSolicitanteDto
    {
        public Guid ID { get; set; }
        public UsuarioDto Usuario { get; set; }
        public Guid UsuarioID { get; set; }
        public DateTime DataCriacao { get; set; }
    }

    public class PropostaLCStatusComiteDto
    {
        public string ID { get; set; }
        public string Nome { get; set; }
        public bool Ativo { get; set; }
    }
}