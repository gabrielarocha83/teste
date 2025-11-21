using System;
using System.Collections.Generic;
// ReSharper disable VirtualMemberCallInConstructor
// ReSharper disable InconsistentNaming

namespace Yara.AppService.Dtos
{
    public class ContaClienteDto : BaseDto
    {
        public string Documento { get; set; }
        public string CodigoPrincipal { get; set; }
        public string Nome { get; set; }
        public string Apelido { get; set; }
        public Guid? SegmentoID { get; set; }
        public virtual SegmentoDto Segmento { get; set; }
        public Guid TipoClienteID { get; set; }
        public virtual TipoClienteDto TipoCliente { get; set; }
        public DateTime DataNascimentoFundacao { get; set; }
        public string Contato { get; set; }
        public string CEP { get; set; }
        public string Endereco { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string UF { get; set; }
        public string Pais { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public bool AdiantamentoLC { get; set; }
        public bool TOP10 { get; set; }
        public bool BloqueioManual { get; set; }
        public bool LiberacaoManual { get; set; }
        public bool RestricaoSerasa { get; set; }
        public RestricaoSerasaDto PendenciaSerasa { get; set; }
        public int TipoConsultaSolicitanteSerasaID { get; set; }
        public Guid SolicitanteSerasaID { get; set; }
        public bool ClientePremium { get; set; }
        public bool Simplificado { get; set; }
        public string Segmentacao { get; set; }
        public string Categorizacao { get; set; }

        // Proposta de Alçada Comercial
        public Guid? PropostaAlcadaID { get; set; }
        public string PropostaAlcadaStatusID { get; set; }

        public bool ExplodeGrupo { get; set; }

        public virtual ICollection<PropostaLCDto> PropostaLcs { get; set; }
        public virtual ICollection<ContaClienteFinanceiroDto> ContaClienteFinanceiro { get; set; }
        public virtual ICollection<ContaClienteEstruturaComercialDto> ContaClienteEstruturaComercial { get; set; }
        public virtual ICollection<ContaClienteRepresentanteDto> ContaClienteRepresentante { get; set; }
        public virtual IEnumerable<ContaClienteCodigoDto> OutrosCodigo { get; set; }
        public virtual ICollection<GrupoEconomicoDto> GrupoEconomicos { get; set; }

        public ContaClienteDto()
        {
            ContaClienteEstruturaComercial = new List<ContaClienteEstruturaComercialDto>();

            ContaClienteRepresentante = new List<ContaClienteRepresentanteDto>();

            ContaClienteFinanceiro = new List<ContaClienteFinanceiroDto>();
            GrupoEconomicos = new List<GrupoEconomicoDto>();
        }
    }
}