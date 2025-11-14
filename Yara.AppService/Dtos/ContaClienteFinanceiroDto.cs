using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Yara.AppService.Dtos
{
    public class ContaClienteFinanceiroDto
    {
        public Guid ContaClienteID { get; set; }
        [JsonIgnore]
        public Guid UsuarioIDCriacao { get; set; }
        [JsonIgnore]
        public Guid? UsuarioIDAlteracao { get; set; }
        [JsonIgnore]
        public DateTime DataCriacao { get; set; }
        [JsonIgnore]
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
        public decimal Recebiveis { get; set; }
        public decimal OperacaoFinanciamento { get; set; }
        public bool Conceito { get; set; }
        public string DescricaoConceito { get; set; }
        public string EmpresasID { get; set; }

        public decimal? Pdd { get; set; }
        public bool Sinistro { get; set; }
        public bool DividaAtiva { get; set; }
        public DateTime? DataSeguradora { get; set; }
        public bool PermitePdd { get; set; }
        public bool PermiteSinistro { get; set; }
        public bool PermiteSerasa { get; set; }
        public bool PermiteConceito { get; set; }
        public bool PermiteEnviarSeguradora { get; set; }
        public bool GrupoEconomicoRestricao { get; set; }

        public virtual ContaClienteDto ContaCliente { get; set; }
        public virtual ConceitoCobrancaDto ConceitoCobranca { get; set; }
        public virtual EmpresasDto Empresas { get; set; }

        public Guid PropostaLCId { get; set; }
        public Guid PropostaLCAdicionalId { get; set; }
        public List<ContaClienteFinanceiroDto> Limites { get; set; }

        public ContaClienteFinanceiroDto()
        {
            Sinistro = false;
            DividaAtiva = false;
            Limites = new List<ContaClienteFinanceiroDto>();
        }
    }
}