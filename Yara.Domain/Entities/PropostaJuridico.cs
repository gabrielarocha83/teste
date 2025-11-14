using System;
using System.Collections.Generic;

namespace Yara.Domain.Entities
{
    public class PropostaJuridico : Base
    {
        public Guid? ContaClienteID { get; set; }
        public int NumeroInternoProposta { get; set; }
        public string ComentarioHistorico { get; set; }
        public string ParecerVisita { get; set; }
        public decimal? ValorEnvio { get; set; }
        public decimal? ValorDebito { get; set; }
        public decimal? PercentualPdd { get; set; }
        public bool ProrrogacaoAnterior { get; set; }
        public bool Aceite { get; set; }
        public bool Protesto { get; set; }
        public bool BuscaBens { get; set; }
        public bool Pedidos { get; set; }
        public bool NotaFiscal { get; set; }
        public bool Comprovante { get; set; }
        public string ParecerCobranca { get; set; }
        public string PropostaJuridicoStatus { get; set; }
        public Guid? ResponsavelID { get; set; }
        public string EmpresaID { get; set; }

        public Empresas Empresa { get; set; }
        public virtual ContaCliente ContaCliente { get; set; }

        public virtual ICollection<PropostaJuridicoTitulo> PropostaJuridicoTitulos { get; set; }
        public virtual ICollection<PropostaJuridicoHistoricoPagamento> PropostaJuridicoHistoricoPagamentos { get; set; }
        public virtual ICollection<PropostaJuridicoGarantia>PropostaJuridicoGarantias { get; set; }
    }
}
