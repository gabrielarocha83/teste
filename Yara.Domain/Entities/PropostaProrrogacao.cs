using System;
using System.Collections.Generic;

namespace Yara.Domain.Entities
{
    public class PropostaProrrogacao : Base
    {
        public int NumeroInternoProposta { get; set; }
        
        public Guid? ContaClienteID { get; set; }
        public Guid? ResponsavelID { get; set; }
        public Guid? MotivoProrrogacaoID { get; set; }
        public Guid? OrigemRecursoID { get; set; }
        public Guid? TipoGarantiaID { get; set; }
        public decimal? TaxaSugerida { get; set; }
        public decimal? ValorProrrogado { get; set; }

        public bool? Favoravel { get; set; }
        public bool? RestricaoSerasa { get; set; }
        public bool? Parcelamento { get; set; }
        public bool? AgregaGarantia { get; set; }

        public string ParecerComercial { get; set; }
        public string ParecerCobranca { get; set; }
        public string PropostaCobrancaStatusID { get; set; }

        public virtual OrigemRecurso OrigemRecurso { get; set; }
        public virtual MotivoProrrogacao MotivoProrrogacao { get; set; }
        public virtual TipoGarantia TipoGarantia { get; set; }
        public virtual IEnumerable<PropostaProrrogacaoTitulo> Titulos { get; set; }
        public virtual IEnumerable<PropostaProrrogacaoParcelamento> Parcelamentos { get; set; }
        public virtual PropostaCobrancaStatus PropostaCobrancaStatus { get; set; }
        public virtual ContaCliente ContaCliente { get; set; }
        public virtual Usuario Responsavel { get; set; }
        public string CodigoSap { get; set; }
        public string EmpresaID { get; set; }
        public virtual Empresas Empresa { get; set; }

        public PropostaProrrogacao()
        {
            Titulos = new List<PropostaProrrogacaoTitulo>();
            Parcelamentos = new List<PropostaProrrogacaoParcelamento>();
        }
    }
}

