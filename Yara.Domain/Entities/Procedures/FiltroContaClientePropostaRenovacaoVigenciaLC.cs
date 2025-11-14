using System;

namespace Yara.Domain.Entities.Procedures
{
    public class FiltroContaClientePropostaRenovacaoVigenciaLC
    {
        public Guid ContaClienteID { get; set; }
        public string EmpresasID { get; set; }
        public string Codigo { get; set; }
        public string NomeCliente { get; set; }
        public string Apelido { get; set; }
        public string Documento { get; set; }
        public string Segmentacao { get; set; }
        public string Categorizacao { get; set; }
        public string NomeGrupo { get; set; }
        public DateTime? DeVigenciaLC { get; set; }
        public DateTime? AteVigenciaLC { get; set; }
        public string Rating { get; set; }
        public decimal? DeValorLC { get; set; }
        public decimal? AteValorLC { get; set; }
        public bool? ClienteTop10Sim { get; set; }
        public bool? ClienteTop10Nao { get; set; }
        public DateTime? DeConsultaSerasa { get; set; }
        public DateTime? AteConsultaSerasa { get; set; }
        public int? RestricaoSerasa { get; set; }
        public bool? RestricoesYaraSim { get; set; }
        public bool? RestricoesYaraNao { get; set; }
        public bool? RestricaoSerasaGrupoSim { get; set; }
        public bool? RestricaoSerasaGrupoNao { get; set; }
        public bool? RestricoesYaraGrupoSim { get; set; }
        public bool? RestricoesYaraGrupoNao { get; set; }
        public bool? LCAndamentoSim { get; set; }
        public bool? LCAndamentoNao { get; set; }
        public bool? AlcadaAndamentoSim { get; set; }
        public bool? AlcadaAndamentoNao { get; set; }
        public DateTime? DeComCompras { get; set; }
        public DateTime? AteComCompras { get; set; }
        public DateTime? DeVigenciaGarantia { get; set; }
        public DateTime? AteVigenciaGarantia { get; set; }
        public Guid? RepresentanteID { get; set; }
        public Guid? AnalistaID { get; set; }
        public string CTC { get; set; }
        public string GC { get; set; }
        public string Diretoria { get; set; }
        public DateTime? DePropostaRenovacao { get; set; }
        public DateTime? AtePropostaRenovacao { get; set; }
        public bool? ClientesRenovadosSim { get; set; }
        public bool? ClientesRenovadosNao { get; set; }
        public string XMLGuidList { get; set; }
    }
}
