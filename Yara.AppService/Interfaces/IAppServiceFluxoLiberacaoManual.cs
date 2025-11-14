using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yara.AppService.Dtos;

namespace Yara.AppService.Interfaces
{
    public interface IAppServiceFluxoLiberacaoManual : IAppServiceBase<FluxoLiberacaoManualDto>
    {
        Task<IEnumerable<FluxoLiberacaoManualDto>> GetAllListFluxoAsync();
        Task<bool> InsertAsync(FluxoLiberacaoManualDto obj);
        Task<bool> Inactive(Guid id);
    }
}
