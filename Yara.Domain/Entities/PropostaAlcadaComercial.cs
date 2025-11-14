using System;
using System.Collections.Generic;

namespace Yara.Domain.Entities
{
    public class PropostaAlcadaComercial : Base
    {
        public Guid? TipoClienteID { get; set; }
        public virtual TipoCliente TipoCliente { get; set; }

        public int NumeroInternoProposta { get; set; }

        public Guid ContaClienteID { get; set; }
        public virtual ContaCliente ContaCliente { get; set; }

        public Guid? ExperienciaID { get; set; }
        public virtual Experiencia Experiencia { get; set; }

        public Guid? TipoGarantiaID { get; set; }
        public virtual TipoGarantia TipoGarantia { get; set; }

        public virtual ICollection<PropostaAlcadaComercialParceriaAgricola> ParceriasAgricolas { get; set; }
        public virtual ICollection<PropostaAlcadaComercialCultura> Culturas { get; set; }
        public virtual ICollection<PropostaAlcadaComercialProdutoServico> Produtos { get; set; }
        public virtual ICollection<PropostaAlcadaComercialDocumentos> Documentos { get; set; }

        public string PorteCliente { get; set; }
        public decimal? FaturamentoAnual { get; set; }

        public string CTC { get; set; }
        public string GC { get; set; }
        public bool TermoAceite { get; set; }
        public string CodigoSap { get; set; }
        public string EstadoCivil { get; set; }
        public string CPFConjugue { get; set; }
        public string NomeConjugue { get; set; }
        public string RegimeCasamento { get; set; }
        public decimal? LCProposto { get; set; }
        public decimal? SharePretendido { get; set; }
        public int? PrazoDias { get; set; }
        public string FontePagamento { get; set; }
        public string ParecerRepresentante { get; set; }

        public string ParecerCTC { get; set; }

        public string ParecerCredito { get; set; }

        public Guid ResponsavelID { get; set; }
        public virtual Usuario Responsavel { get; set; }

        public Guid? SolicitanteSerasaPropostaID { get; set; }
        public SolicitanteSerasa SolicitanteSerasaProposta { get; set; }
        public TipoSerasa TipoSerasaID { get; set; }

        public Guid? SolicitanteSerasaConjugeID { get; set; }
        public SolicitanteSerasa SolicitanteSerasaConjuge { get; set; }
        public TipoSerasa TipoSerasaConjugeID { get; set; }

        public bool RestricaoSerasa { get; set; }
        public string DetalheRestricaoSerasa { get; set; }

        public DateTime? DataFundacao { get; set; }

        public string PropostaCobrancaStatusID { get; set; }
        public virtual PropostaCobrancaStatus PropostaCobrancaStatus { get; set; }

        public string EmpresaID { get; set; }
        public Empresas Empresa { get; set; }

        public string Comentario { get; set; }

        public PropostaAlcadaComercial()
        {
            Documentos = new List<PropostaAlcadaComercialDocumentos>();
            ParceriasAgricolas = new List<PropostaAlcadaComercialParceriaAgricola>();
        }
    }
}