using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Yara.AppService.Dtos
{
    public class PropostaLCDto : BaseDto
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
        public virtual ICollection<PropostaLCParceriaAgricolaDto> ParceriasAgricolas { get; set; }
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
        public virtual ICollection<PropostaLCGarantiaDto> Garantias { get; set; }
        public virtual ICollection<PropostaLCCulturaDto> Culturas { get; set; }
        public virtual ICollection<PropostaLCNecessidadeProdutoDto> NecessidadeProduto { get; set; }
        public virtual ICollection<PropostaLCMercadoDto> PrincipaisMercados { get; set; }
        public bool? PossuiCriacaoDeAnimais { get; set; }
        public virtual ICollection<PropostaLCPecuariaDto> CriacoesAnimais { get; set; }
        public bool? PossuiOutrasReceitas { get; set; }
        public virtual ICollection<PropostaLCOutraReceitaDto> OutrasReceitas { get; set; }
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
        public virtual ICollection<PropostaLCReferenciaDto> Referencias { get; set; }
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
        public RepresentanteDto Representante { get; set; }
        public Guid? RepresentanteID { get; set; }

        public decimal? LCCliente { get; set; }
        public DateTime? VigenciaInicialCliente { get; set; }
        public DateTime? VigenciaFinalCliente { get; set; }
        public string RatingCliente { get; set; }

        public bool AprovadoComite { get; set; }
        public DateTime? DataAprovacaoComite { get; set; }
        public string Rating { get; set; }
        public decimal? PotencialCredito { get; set; }
        public virtual ICollection<PropostaLCBemRuralDto> BensRurais { get; set; }
        public virtual ICollection<PropostaLCBemUrbanoDto> BensUrbanos { get; set; }
        public virtual ICollection<PropostaLCMaquinaEquipamentoDto> MaquinasEquipamentos { get; set; }

        public decimal? ValorTotalBensRurais { get; set; }
        public decimal? ValorTotalBensUrbanos { get; set; }
        public decimal? ValorTotalMaquinasEquipamentos { get; set; }

        public string ComentarioPatrimonio { get; set; }
        public virtual ICollection<PropostaLCPrecoPorRegiaoDto> PrecosPorRegiao { get; set; }
        public string PropostaLCStatusID { get; set; }
        public virtual ICollection<PropostaLCTipoEndividamentoDto> TiposEndividamento { get; set; }
        public virtual UsuarioDto Responsavel { get; set; }
        public Guid? ResponsavelID { get; set; }

        public string CodigoSap { get; set; }
        public Guid? PropostaLCDemonstrativoID { get; set; }

        public Guid? SolicitanteSerasaID { get; set; }
        public virtual SolicitanteSerasaDto SolicitanteSerasa { get; set; }
        public TipoSerasaDto TipoSerasaID { get; set; }

        public Guid? SolicitanteSerasaConjugeID { get; set; }
        public virtual SolicitanteSerasaDto SolicitanteSerasaConjuge { get; set; }
        public TipoSerasaDto TipoSerasaConjugeID { get; set; }

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

        // Navigation Properties
        [JsonIgnore]
        public ContaClienteDto ContaCliente { get; set; }

        public virtual TipoClienteDto TipoCliente { get; set; }
        public virtual ExperienciaDto Experiencia { get; set; }
        public virtual AreaIrrigadaDto AreaIrrigada { get; set; }
        public virtual IdadeMediaCanavialDto IdadeMediaCanavial { get; set; }
        public virtual PropostaLCStatusDto PropostaLcStatus { get; set; }
        public new DateTime DataCriacao { get; set; }

        public string NumeroProposta
        {
            get
            {
                return string.Format("LC{0:00000}/{1:yyyy}", this.NumeroInternoProposta, this.DataCriacao);
            }
        }

        // Constructor
        public PropostaLCDto()
        {
            ParceriasAgricolas = new List<PropostaLCParceriaAgricolaDto>();
            Garantias = new List<PropostaLCGarantiaDto>();
            Culturas = new List<PropostaLCCulturaDto>();
            NecessidadeProduto = new List<PropostaLCNecessidadeProdutoDto>();
            PrincipaisMercados = new List<PropostaLCMercadoDto>();
            CriacoesAnimais = new List<PropostaLCPecuariaDto>();
            OutrasReceitas = new List<PropostaLCOutraReceitaDto>();
            Referencias = new List<PropostaLCReferenciaDto>();
            BensRurais = new List<PropostaLCBemRuralDto>();
            BensUrbanos = new List<PropostaLCBemUrbanoDto>();
            MaquinasEquipamentos = new List<PropostaLCMaquinaEquipamentoDto>();
            PrecosPorRegiao = new List<PropostaLCPrecoPorRegiaoDto>();
            TiposEndividamento = new List<PropostaLCTipoEndividamentoDto>();
        }
    }
}