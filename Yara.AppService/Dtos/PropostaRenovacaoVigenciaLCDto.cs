using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

#pragma warning disable CS0108 // O membro oculta o membro herdado; nova palavra-chave ausente

namespace Yara.AppService.Dtos
{
    public class PropostaRenovacaoVigenciaLCDto : BaseDto
    {
        // Base
        public Guid ID { get; set; }
        public DateTime DataCriacao { get; set; }
        public Guid UsuarioIDCriacao { get; set; }
        public Guid? UsuarioIDAlteracao { get; set; }
        public DateTime? DataAlteracao { get; set; }

        // Dto Only
        public string UsuarioNomeCriacao { get; set; }
        
        // PropostaRenovacaoVigenciaLC
        public int NumeroInternoProposta { get; set; }

        public string PropostaLCStatusID { get; set; }

        public Guid ResponsavelID { get; set; }
        public string ResponsavelNome { get; set; }

        public decimal Montante { get; set; }
        public DateTime DataNovaVigencia { get; set; }

        [JsonIgnore]
        public virtual ICollection<PropostaRenovacaoVigenciaLCClienteDto> Clientes { get; set; }

        public string EmpresaID { get; set; }

        // Navigation Properties
        public virtual UsuarioDto Responsavel { get; set; }
        public virtual EmpresasDto Empresa { get; set; }
        public virtual PropostaLCStatusDto PropostaLCStatus { get; set; }

        public List<PropostaRenovacaoVigenciaLCClienteDto> ClientesAptos
        {
            get
            {
                return Clientes.Where(c => c.Apto).ToList();
            }
        }

        public List<PropostaRenovacaoVigenciaLCClienteDto> ClientesNaoAptos
        {
            get
            {
                return Clientes.Where(c => !c.Apto).ToList();
            }
        }

        public int QuantidadeClientes
        {
            get
            {
                return Clientes.Count;
            }
        }

        public string NumeroProposta
        {
            get
            {
                return string.Format("RV{0:00000}/{1:yyyy}", this.NumeroInternoProposta, this.DataCriacao);
            }
        }

        public PropostaRenovacaoVigenciaLCDto()
        {
            Clientes = new List<PropostaRenovacaoVigenciaLCClienteDto>();
        }
    }

    public class ListaClientePropostaRenovacaoVigenciaLCDto
    {
        public DateTime DataNovaVigencia { get; set; }
        public IEnumerable<Guid> ContaClienteID { get; set; }

        public ListaClientePropostaRenovacaoVigenciaLCDto()
        {
            ContaClienteID = new List<Guid>();
        }
    }
}
