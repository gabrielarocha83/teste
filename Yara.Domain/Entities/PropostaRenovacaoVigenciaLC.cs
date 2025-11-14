using System;
using System.Collections.Generic;

namespace Yara.Domain.Entities
{
    public class PropostaRenovacaoVigenciaLC : Base
    {
        public int NumeroInternoProposta { get; set; }

        public string PropostaLCStatusID { get; set; }

        public Guid ResponsavelID { get; set; }

        public decimal Montante { get; set; }
        public DateTime DataNovaVigencia { get; set; }

        public virtual ICollection<PropostaRenovacaoVigenciaLCCliente> Clientes { get; set; }

        public string EmpresaID { get; set; }

        public virtual Usuario Responsavel { get; set; }
        public virtual Empresas Empresa { get; set; }
        public virtual PropostaLCStatus PropostaLCStatus { get; set; }

        public PropostaRenovacaoVigenciaLC()
        {
            Clientes = new List<PropostaRenovacaoVigenciaLCCliente>();
        }
    }
}
