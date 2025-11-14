using System;
using System.Threading.Tasks;
using Yara.AppService.Dtos;

namespace Yara.AppService.Interfaces
{
    public interface IAppServiceFluxoLiberacaoProrrogacao : IAppServiceBase<FluxoLiberacaoProrrogacaoDto>
    {
        Task<bool> InsertAsync(FluxoLiberacaoProrrogacaoDto obj);
        Task<bool> Inactive(Guid id, Guid userIdAlteracao);
    }
}
