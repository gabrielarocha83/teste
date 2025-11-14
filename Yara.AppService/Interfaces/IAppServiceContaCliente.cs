using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yara.AppService.Dtos;

namespace Yara.AppService.Interfaces
{
    public interface IAppServiceContaCliente : IAppServiceBase<ContaClienteDto>
    {
        Task<IEnumerable<BuscaContaClienteDto>> GetListAccountClient(BuscaContaClienteDto buscaContaClienteDto, Guid usuarioId);
        Task<IEnumerable<BuscaContaClienteEstComlDto>> GetListByComlStruc(BuscaContaClienteEstComlDto busca);
        Task<ContaClienteDto> GetByID(Guid id, Guid usuarioId, string empresaId);
        Task<ContaClienteDto> GetByCodePrincipal(string code);
        Task<Guid?> GetIdByCode(string code);
        Task<string> UpdateAsync(ContaClienteAlteracaoDadosPessoaisDto obj);
        Task<bool> UpdateAsyncManualLock(BloqueioManualContaClienteDto obj);
        Task<bool> UpdateAsyncAllowManualLock(ContaClienteDto obj);
        Task<bool> UpdateEstruturaContaCliente(MovimentacaoEstruturaComercialDto obj);
        Task<bool> UpdateRepresentanteContaCliente(MovimentacaoEstruturaComercialDto obj);
        Task<bool> InsertAsync(ContaClienteDto obj);
        BuscaOrdemVendaSumarizadoDto GetOrdemVendaSumarizado(Guid ContaClienteID, string Empresa);
        Task<IEnumerable<BuscaOrdemVendasPrazoDto>> GetOrdemVendaPorClientePrazo(BuscaOrdemVendasPrazoDto ordemVendasPrazo);
        Task<IEnumerable<BuscaOrdemVendasAVistaDto>> GetOrdemVendaPorClienteVista(BuscaOrdemVendasAVistaDto ordemVendasPrazo);
        Task<IEnumerable<BuscaOrdemVendasPagaRetiraDto>> GetOrdemVendaPorClienteRetira(BuscaOrdemVendasPagaRetiraDto ordemVendasPrazo);
        Task<IEnumerable<TitulosGrupoEconomicoMembrosDto>> TitulosGrupoEconomicoMembroContaCliente(Guid contaClienteId, string empresa);
        Task<PropostaAtualDto> ValidProposalReturn(Guid id, string empresa);
        Task<bool> ReavaliarContaCliente(Guid id, string empresa);
    }
}
