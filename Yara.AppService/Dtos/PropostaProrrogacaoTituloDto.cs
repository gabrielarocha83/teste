using System;

namespace Yara.AppService.Dtos
{
    public class PropostaProrrogacaoTituloDto
    {
        public Guid ID { get; set; }

        public DateTime? VencimentoProrrogado { get; set; }
        public string NotaFiscal { get; set; }
        public string NumeroDocumento { get; set; }
        public string Linha { get; set; }
        public string AnoExercicio { get; set; }
        public string Empresa { get; set; }
        public string OrdemVendaNumero { get; set; }
        public DateTime? DataEmissaoDocumento { get; set; }
        public string CondPagto { get; set; }
        public decimal ValorInterno { get; set; }
        public DateTime? VencimentoOriginal { get; set; }
        public string UltimoComentario { get; set; }
        public string TipoPR { get; set; }
        public DateTime? DataAceite { get; set; }
        public bool NaoCobranca { get; set; }
        public DateTime NovoVencimento { get; set; }

        public Guid ContaClienteID { get; set; }
        public Guid PropostaProrrogacaoID { get; set; }
        public PropostaProrrogacaoDto PropostaProrrogacao { get; set; }
    }
}