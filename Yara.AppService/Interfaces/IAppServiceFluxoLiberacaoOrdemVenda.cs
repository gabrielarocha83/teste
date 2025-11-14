using System;
using System.Threading.Tasks;
using Yara.AppService.Dtos;

namespace Yara.AppService.Interfaces
{
    public interface IAppServiceFluxoLiberacaoOrdemVenda : IAppServiceBase<FluxoLiberacaoOrdemVendaDto>
    {
        Task<bool> InsertAsync(FluxoLiberacaoOrdemVendaDto obj);
        Task<bool> Inactive(Guid id, Guid userIdAlteracao);
    }
}
