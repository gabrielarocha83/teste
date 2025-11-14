using System;

namespace Yara.Domain.Entities
{
    public class PropostaProrrogacaoTitulo
    {
        public Guid ID { get; set; }

        public DateTime? VencimentoProrrogado { get; set; }
        public string NotaFiscal { get; set; }
        public string NumeroDocumento { get; set; }
        public string Linha { get; set; }
        public string AnoExercicio { get; set; }
        public string Empresa { get; set; }
        public string Pedido { get; set; }
        public DateTime? Emissao { get; set; }
        public string PayT { get; set; }
        public decimal Valor { get; set; }
        public DateTime? VencimentoOriginal { get; set; }
        public string ComentarioHistorico { get; set; }
        public string PRRPR { get; set; }
        public DateTime? Aceite { get; set; }
        public bool NaoCobranca { get; set; }
        public DateTime NovoVencimento { get; set; }

        public Guid ContaClienteID { get; set; }
        public Guid PropostaProrrogacaoID { get; set; }
        public PropostaProrrogacao PropostaProrrogacao { get; set; }
    }
}