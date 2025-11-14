using System;
using System.Collections.Generic;

namespace Yara.Domain.Entities
{
    public class PropostaAbono : Base
    {
        public Guid? MotivoAbonoID { get; set; }
        public Guid ContaClienteID { get; set; }
        public Guid ResponsavelID { get; set; }
        // public bool Acompanhar { get; set; }
        public int NumeroInternoProposta { get; set; }
        public bool ConceitoH { get; set; }
        public string ParecerComercial { get; set; }
        public string ParecerCobranca { get; set; }
        public string PropostaCobrancaStatusID { get; set; }
        public string Motivo { get; set; }
        public decimal ValorTotalDocumento { get; set; }
        public string EmpresaID { get; set; }
        public decimal TotalDebito { get; set; }
        public bool Sinistro { get; set; }
        public string CodigoSap { get; set; }
        public virtual MotivoAbono MotivoAbono { get; set; }
        public virtual PropostaCobrancaStatus PropostaCobrancaStatus { get; set; }
        public virtual IEnumerable<PropostaAbonoTitulo> Titulos { get; set; }

        public Usuario Responsavel { get; set; }
        public virtual ContaCliente ContaCliente { get; set; }
        public virtual Empresas Empresa { get; set; }
    }
}
