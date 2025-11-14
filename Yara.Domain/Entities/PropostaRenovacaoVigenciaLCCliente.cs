using System;

namespace Yara.Domain.Entities
{
    public class PropostaRenovacaoVigenciaLCCliente
    {
        public Guid ID { get; set; }

        public Guid ContaClienteID { get; set; }
        public Guid PropostaRenovacaoVigenciaLCID { get; set; }
        public string NomeCliente { get; set; }
        public string CodigoCliente { get; set; }
        public string Apelido { get; set; }
        public string Documento { get; set; }
        public string TipoCliente { get; set; }
        public string NomeGrupo { get; set; }
        public string ClassificacaoGrupo { get; set; }
        public DateTime? DataVigenciaLC { get; set; }
        public string Rating { get; set; }
        public decimal? ValorLC { get; set; }
        public bool? Top10 { get; set; }
        public DateTime? DataConsultaSerasa { get; set; }
        public int? RestricaoSerasa { get; set; }
        public bool? RestricaoYara { get; set; }
        public bool? RestricaoSerasaGrupo { get; set; }
        public bool? RestricaoYaraGrupo { get; set; }
        public string CodigoPropostaLC { get; set; }
        public decimal? ValorPropostaLC { get; set; }
        public string PropostaLCStatus { get; set; }
        public string CodigoPropostaAC { get; set; }
        public decimal? ValorPropostaAC { get; set; }
        public string PropostaACStatus { get; set; }
        public DateTime? DataUltimaCompra { get; set; }
        public DateTime? DataValidadeGarantia { get; set; }
        public string Representante { get; set; }
        public string CTC { get; set; }
        public string GC { get; set; }
        public string Diretoria { get; set; }
        public string Analista { get; set; }
        public bool Apto { get; set; }

        public virtual PropostaRenovacaoVigenciaLC PropostaRenovacaoVigenciaLC { get; set; }
        public virtual ContaCliente ContaCliente { get; set; }
    }
}
