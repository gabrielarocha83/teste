using System;
using System.Collections.Generic;
using Yara.AppService.Dtos;

namespace Yara.AppService.Dtos
{
    public class PropostaProrrogacaoInserirDto : BaseDto
    {
        public Guid ContaClienteID { get; set; }
        public IEnumerable<PropostaProrrogacaoTituloDto> Titulos { get; set; }
        public string EmpresaID { get; set; }
        public DateTime NovoVencimento { get; set; }
    }

    public class AprovaReprovaProrrogacaoDto
    {
        public Guid FluxoID { get; set; }
        public Guid PropostaProrrogacaoID { get; set; }
        public string EmpresaID { get; set; }
        public Guid UsuarioID { get; set; }
        public bool NovasLiberacoes { get; set; }
        public bool Aprovado { get; set; }
        public float TaxaJuros { get; set; }
        public string Comentario { get; set; }
    }

    public class PropostaProrrogacaoDto : BaseDto
    {
        public int NumeroInternoProposta { get; set; }
        public bool Acompanhar { get; set; }
        public Guid? ContaClienteID { get; set; }
        public Guid? ResponsavelID { get; set; }
        public Guid? MotivoProrrogacaoID { get; set; }
        public Guid? OrigemRecursoID { get; set; }
        public Guid? TipoGarantiaID { get; set; }
        public decimal? TaxaSugerida { get; set; }
        public decimal? ValorProrrogado { get; set; }

        public bool? Favoravel { get; set; }
        public bool? RestricaoSerasa { get; set; }
        public bool? Parcelamento { get; set; }
        public bool? AgregaGarantia { get; set; }

        public string ParecerComercial { get; set; }
        public string ParecerCobranca { get; set; }
        public string PropostaCobrancaStatusID { get; set; }
        public string Motivo { get; set; }
        public virtual OrigemRecursoDto OrigemRecurso { get; set; }
        public virtual MotivoProrrogacaoDto MotivoProrrogacao { get; set; }
        public virtual TipoGarantiaDto TipoGarantia { get; set; }
        public virtual IEnumerable<PropostaProrrogacaoTituloDto> Titulos { get; set; }
        public virtual IEnumerable<PropostaProrrogacaoParcelamentoDto> Parcelamentos { get; set; }
        public virtual PropostaCobrancaStatusDto PropostaCobrancaStatus { get; set; }
        public virtual ContaClienteDto ContaCliente { get; set; }
        public virtual UsuarioDto Responsavel { get; set; }
        public string CodigoSap { get; set; }
        public string EmpresaID { get; set; }
        public virtual EmpresasDto Empresa { get; set; }
        public new DateTime DataCriacao { get; set; }

        public string NumeroProposta
        {
            get
            {
                return string.Format("PR{0:00000}/{1:yyyy}", this.NumeroInternoProposta, this.DataCriacao);
            }
        }

        public PropostaProrrogacaoDto()
        {
            Titulos = new List<PropostaProrrogacaoTituloDto>();
            Parcelamentos = new List<PropostaProrrogacaoParcelamentoDto>();
        }
    }
}

