using System;

namespace Yara.Domain.Entities
{
    public class ContaClienteFinanceiro
    {
        public Guid ContaClienteID { get; set; }
        public Guid UsuarioIDCriacao { get; set; }
        public Guid? UsuarioIDAlteracao { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataAlteracao { get; set; }

        public decimal? LC { get; set; }
        public DateTime? Vigencia { get; set; }
        public DateTime? VigenciaFim { get; set; }
        public decimal? Exposicao { get; set; }
        public decimal? ExposicaoRemessas { get; set; }
        public decimal? ExposicaoVendaOrdem { get; set; }
        public decimal? ExposicaoExportacao { get; set; }
        public decimal? LCAdicional { get; set; }
        public DateTime? VigenciaAdicional { get; set; }
        public DateTime? VigenciaAdicionalFim { get; set; }
        public decimal? LCAnterior { get; set; }
        public DateTime? VigenciaAnterior { get; set; }
        public DateTime? VigenciaFimAnterior { get; set; }

        public string Rating { get; set; }

        public Guid? ConceitoCobrancaID { get; set; }
        public decimal? Recebiveis { get; set; }
        public decimal? OperacaoFinanciamento { get; set; }
        public bool Conceito { get; set; }
        public string DescricaoConceito { get; set; }
        public string EmpresasID { get; set; }
        public decimal? Pdd { get; set; }
        public bool Sinistro { get; set; }
        public bool DividaAtiva { get; set; }
        public DateTime? DataSeguradora { get; set; }
        public Guid? ConceitoCobrancaIDAnterior { get; set; }
        public bool ConceitoAnterior { get; set; }
        public string DescricaoConceitoAnterior { get; set; }
        public bool GrupoEconomicoRestricao { get; set; }

        public virtual ContaCliente ContaCliente { get; set; }
        public virtual ConceitoCobranca ConceitoCobranca { get; set; }
        public virtual Empresas Empresas { get; set; }

        public ContaClienteFinanceiro()
        {
            Sinistro = false;
            DividaAtiva = false;
        }
    }
}