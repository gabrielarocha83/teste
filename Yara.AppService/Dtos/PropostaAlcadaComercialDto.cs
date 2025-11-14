using System;
using System.Collections.Generic;

namespace Yara.AppService.Dtos
{
    public class PropostaAlcadaComercialDto 
    {
        // Base
        public Guid ID { get; set; }
        public DateTime DataCriacao { get; set; }
        public Guid UsuarioIDCriacao { get; set; }
        public Guid? UsuarioIDAlteracao { get; set; }
        public DateTime? DataAlteracao { get; set; }

        // PropostaAlcadaComercial
        public Guid? TipoClienteID { get; set; }
        public string TipoClienteNome { get; set; }
        public int NumeroInternoProposta { get; set; }
        public Guid ContaClienteID { get; set; }
        public string ClienteNome { get; set; }
        public Guid? ExperienciaID { get; set; }
        public string ExperienciaNome { get; set; }
        public string DocumentoContaCliente { get; set; }

        public Guid? TipoGarantiaaID { get; set; }
        public string TipoGarantiaNome { get; set; }

        public virtual ICollection<PropostaAlcadaComercialParceriaAgricolaDto> ParceriasAgricolas { get; set; }
        public virtual ICollection<PropostaAlcadaComercialCulturaDto> Culturas { get; set; }
        public virtual ICollection<PropostaAlcadaComercialProdutoServicoDto> Produtos { get; set; }
        public virtual ICollection<PropostaAlcadaComercialDocumentosDto> Documentos { get; set; }

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
        public string ResponsavelNome { get; set; }

        public Guid? SolicitanteSerasaPropostaID { get; set; }
        public TipoSerasaDto TipoSerasaID { get; set; }

        public Guid? SolicitanteSerasaConjugeID { get; set; }
        public TipoSerasaDto TipoSerasaConjugeID { get; set; }

        public bool RestricaoSerasa { get; set; }
        public string DetalheRestricaoSerasa { get; set; }

        public DateTime? DataFundacao { get; set; }

        public string PropostaCobrancaStatusID { get; set; }
        public string PropostaStatus { get; set; }

        public string EmpresaID { get; set; }
        public bool Acompanhar { get; set; }
        public string Comentario { get; set; }

        public string NumeroProposta
        {
            get
            {
                return string.Format("AC{0:00000}/{1:yyyy}", this.NumeroInternoProposta, this.DataCriacao);
            }
        }
    }
}