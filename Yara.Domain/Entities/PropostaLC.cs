using System;
using System.Collections.Generic;

namespace Yara.Domain.Entities
{
    public class PropostaLC : Base
    {
        public int NumeroInternoProposta { get; set; }
        public string EmpresaID { get; set; }
        public Guid ContaClienteID { get; set; }
        public Guid? TipoClienteID { get; set; }
        public Guid? ExperienciaID { get; set; }
        public string EstadoCivil { get; set; }
        public string CPFConjugue { get; set; }
        public string NomeConjugue { get; set; }
        public string RegimeCasamento { get; set; }
        public virtual ICollection<PropostaLCParceriaAgricola> ParceriasAgricolas { get; set; }
        public bool? PossuiGerenteTecnico { get; set; }
        public bool? PossuiMaquinarioProprio { get; set; }
        public bool? PossuiArmazem { get; set; }
        public decimal? ToneladasArmazem { get; set; }
        public bool? PossuiAreaIrrigada { get; set; }
        public Guid? AreaIrrigadaID { get; set; }
        public bool? PossuiContratoProximaSafra { get; set; }
        public string Trading { get; set; }
        public decimal? QtdeSacas { get; set; }
        public decimal? PrecoSaca { get; set; }
        public bool? ClienteGarantia { get; set; }
        public virtual ICollection<PropostaLCGarantia> Garantias { get; set; }
        public virtual ICollection<PropostaLCCultura> Culturas { get; set; }
        public virtual ICollection<PropostaLCNecessidadeProduto> NecessidadeProduto { get; set; }
        public virtual ICollection<PropostaLCMercado> PrincipaisMercados { get; set; }
        public bool? PossuiCriacaoDeAnimais { get; set; }
        public virtual ICollection<PropostaLCPecuaria> CriacoesAnimais { get; set; }
        public bool? PossuiOutrasReceitas { get; set; }
        public virtual ICollection<PropostaLCOutraReceita> OutrasReceitas { get; set; }
        public string PrincipaisFornecedores { get; set; }
        public string PrincipaisClientes { get; set; }
        public string ComentarioMercado { get; set; }
        public decimal? NecessidadeAnualFertilizantes { get; set; }
        public decimal? PrecoMedioFt { get; set; }
        public decimal? NecessidadeAnualFoliar { get; set; }
        public decimal? PrecoMedioLt { get; set; }
        public int? NumeroComprasAno { get; set; }
        public decimal? ResultadoUltimaSafra { get; set; }
        public int? NumeroClienteCooperados { get; set; }
        public int? PrazoMedioVendas { get; set; }
        public Guid? IdadeMediaCanavialID { get; set; }
        public decimal? TotalProducaoAlcool { get; set; }
        public decimal? TotalProducaoAcucar { get; set; }
        public decimal? VolumeMoagemPropria { get; set; }
        public decimal? CustoMedioProducao { get; set; }
        public decimal? CapacidadeMoagem { get; set; }
        public decimal? TotalMWAno { get; set; }
        public decimal? CustoCarregamentoTransporte { get; set; }
        public virtual ICollection<PropostaLCReferencia> Referencias { get; set; }
        public string RestricaoSERASA { get; set; }
        public string RestricaoTJ { get; set; }
        public string RestricaoIBAMA { get; set; }
        public string RestricaoTrabalhoEscravo { get; set; }
        public decimal? LCProposto { get; set; }
        public decimal? SharePretentido { get; set; }
        public int? PrazoEmDias { get; set; }
        public string FonteRecursosCarteira { get; set; }
        public string ParecerRepresentante { get; set; }
        public string ParecerCTC { get; set; }
        public Representante Representante { get; set; }
        public Guid? RepresentanteID { get; set; }

        public decimal? LCCliente { get; set; }
        public DateTime? VigenciaInicialCliente { get; set; }
        public DateTime? VigenciaFinalCliente { get; set; }
        public string RatingCliente { get; set; }

        public bool AprovadoComite { get; set; }
        public DateTime? DataAprovacaoComite { get; set; }
        public string Rating { get; set; }
        public decimal? PotencialCredito { get; set; }
        public virtual ICollection<PropostaLCBemRural> BensRurais { get; set; }
        public virtual ICollection<PropostaLCBemUrbano> BensUrbanos { get; set; }
        public virtual ICollection<PropostaLCMaquinaEquipamento> MaquinasEquipamentos { get; set; }

        public decimal? ValorTotalBensRurais { get; set; }
        public decimal? ValorTotalBensUrbanos { get; set; }
        public decimal? ValorTotalMaquinasEquipamentos { get; set; }

        public string ComentarioPatrimonio { get; set; }
        public virtual ICollection<PropostaLCPrecoPorRegiao> PrecosPorRegiao { get; set; }
        public string PropostaLCStatusID { get; set; }
        public virtual ICollection<PropostaLCTipoEndividamento> TiposEndividamento { get; set; }
        public virtual Usuario Responsavel { get; set; }
        public Guid? ResponsavelID { get; set; }

        public string CodigoSap { get; set; }
        public Guid? PropostaLCDemonstrativoID { get; set; }

        public SolicitanteSerasa SolicitanteSerasa { get; set; }
        public Guid? SolicitanteSerasaID { get; set; }
        public TipoSerasa TipoSerasaID { get; set; }

        public Guid? SolicitanteSerasaConjugeID { get; set; }
        public SolicitanteSerasa SolicitanteSerasaConjuge { get; set; }
        public TipoSerasa TipoSerasaConjugeID { get; set; }

        public bool RestricaoSerasaFlag { get; set; }
        public string DescricaoRestricao { get; set; }

        public decimal? FixarLimiteCredito { get; set; }

        public string ParecerAnalista { get; set; }

        public decimal? ReceitaTotal { get; set; }
        public bool BalancoAuditado { get; set; }
        public string EmpresaAuditora { get; set; }
        public bool Ressalvas { get; set; }
        public Guid? DemonstrativoFinanceiroID { get; set; }

        public bool AnaliseGrupo { get; set; }
        public Guid? GrupoEconomicoID { get; set; }
        public bool AcompanharProposta { get; set; }
        public string Documento { get; set; }

        public decimal? PotencialPatrimonial { get; set; }
        public decimal? PotencialReceita { get; set; }

        public bool Ecomm { get; set; }

        // Navigation Properties
        public ContaCliente ContaCliente { get; set; }
        public virtual TipoCliente TipoCliente { get; set; }
        public virtual Experiencia Experiencia { get; set; }
        public virtual AreaIrrigada AreaIrrigada { get; set; }
        public virtual IdadeMediaCanavial IdadeMediaCanavial { get; set; }
        public virtual PropostaLCStatus PropostaLcStatus { get; set; }

        // Constructor
        public PropostaLC()
        {
            ParceriasAgricolas = new List<PropostaLCParceriaAgricola>();
            Garantias = new List<PropostaLCGarantia>();
            Culturas = new List<PropostaLCCultura>();
            NecessidadeProduto = new List<PropostaLCNecessidadeProduto>();
            PrincipaisMercados = new List<PropostaLCMercado>();
            CriacoesAnimais = new List<PropostaLCPecuaria>();
            OutrasReceitas = new List<PropostaLCOutraReceita>();
            Referencias = new List<PropostaLCReferencia>();
            BensRurais = new List<PropostaLCBemRural>();
            BensUrbanos = new List<PropostaLCBemUrbano>();
            MaquinasEquipamentos = new List<PropostaLCMaquinaEquipamento>();
            PrecosPorRegiao = new List<PropostaLCPrecoPorRegiao>();
            TiposEndividamento = new List<PropostaLCTipoEndividamento>();
        }
    }
}