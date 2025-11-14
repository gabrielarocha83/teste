using System;
using System.Collections.Generic;

namespace Yara.AppService.Dtos
{
    public class PropostaLCAdicionalComiteDto
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

        public string EmpresaID { get; set; }
        public string CodigoSAP { get; set; }

        public ICollection<string> Aprovadores { get; set; }

        public Guid PropostaLCAdicionalID { get; set; }
        public Guid PerfilID { get; set; }
        public Guid UsuarioID { get; set; }
        public Guid PropostaLCAdicionalComiteSolicitanteID { get; set; }
        public Guid FluxoLiberacaoLCAdicionalID { get; set; }
        public string PropostaLCAdicionalStatusComiteID { get; set; }

        public virtual PropostaLCAdicionalDto PropostaLCAdicional { get; set; }
        public virtual PerfilDto Perfil { get; set; }
        public virtual UsuarioDto Usuario { get; set; }
        public virtual PropostaLCAdicionalComiteSolicitanteDto PropostaLCAdicionalComiteSolicitante { get; set; }
        public virtual FluxoLiberacaoLCAdicionalDto FluxoLiberacaoLCAdicional { get; set; }
        public virtual PropostaLCAdicionalStatusComiteDto PropostaLCAdicionalStatusComite { get; set; }
    }

    public class PropostaLCAdicionalComiteSolicitanteDto
    {
        public Guid ID { get; set; }
        public UsuarioDto Usuario { get; set; }
        public Guid UsuarioID { get; set; }
        public DateTime DataCriacao { get; set; }
    }

    public class PropostaLCAdicionalStatusComiteDto
    {
        public string ID { get; set; }
        public string Nome { get; set; }
        public bool Ativo { get; set; }
    }
}
