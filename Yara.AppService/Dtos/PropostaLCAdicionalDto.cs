using System;
using System.Collections.Generic;

namespace Yara.AppService.Dtos
{
    public class PropostaLCAdicionalDto : BaseDto
    {

        public int NumeroInternoProposta { get; set; }
        public string EmpresaID { get; set; }
        public decimal? LCAdicional { get; set; }
        public DateTime? VigenciaAdicional { get; set; }
        public string Parecer { get; set; }
        public bool AcompanharProposta { get; set; }
        public string CodigoSap { get; set; }

        public decimal? LCCliente { get; set; }
        public DateTime? VigenciaInicialCliente { get; set; }
        public DateTime? VigenciaFinalCliente { get; set; }

        public bool AprovadoComite { get; set; }
        public DateTime? DataAprovacaoComite { get; set; }
        public decimal? FixarLimiteCredito { get; set; }

        public Guid ContaClienteID { get; set; }
        public Guid? TipoClienteID { get; set; }
        public Guid? ResponsavelID { get; set; }
        public string PropostaLCStatusID { get; set; }
        public Guid? GrupoEconomicoID { get; set; }

        // Custom Dto Properties
        public Guid UsuarioIDCriacao { get; set; }
        public DateTime DataCriacao { get; set; }
        public string ResponsavelNome { get; set; }
        public string UsuarioCriacaoNome { get; set; }
        public ICollection<Guid> UsuarioIdAcompanhamento { get; set; } = new List<Guid>();

        // Custom Dto Getters
        public string NumeroProposta
        {
            get
            {
                return string.Format("LA{0:00000}/{1:yyyy}", this.NumeroInternoProposta, this.DataCriacao);
            }
        }

        public virtual ContaClienteDto ContaCliente { get; set; }
        public virtual TipoClienteDto TipoCliente { get; set; }
        public virtual UsuarioDto Responsavel { get; set; }
        public virtual PropostaLCStatusDto PropostaLCStatus { get; set; }
        public virtual GrupoEconomicoDto GrupoEconomico { get; set; }
    }
}
