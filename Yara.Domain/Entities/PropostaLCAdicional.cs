using System;

namespace Yara.Domain.Entities
{
    public class PropostaLCAdicional : Base
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

        public virtual ContaCliente ContaCliente { get; set; }
        public virtual TipoCliente TipoCliente { get; set; }
        public virtual Usuario Responsavel { get; set; }
        public virtual PropostaLCStatus PropostaLCStatus { get; set; }
        public virtual GrupoEconomico GrupoEconomico { get; set; }
    }
}
