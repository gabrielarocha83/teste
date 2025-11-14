using System;
using System.Threading.Tasks;
using Yara.AppService.Dtos;

namespace Yara.AppService.Interfaces
{
    public interface IAppServiceFluxoRenovacaoVigenciaLC : IAppServiceBase<FluxoRenovacaoVigenciaLCDto>
    {
        Task<bool> InsertAsync(FluxoRenovacaoVigenciaLCDto fluxoRenovacaoVigenciaLCDto);
        Task<bool> RemoveAsync(FluxoRenovacaoVigenciaLCDto fluxoRenovacaoVigenciaLCDto);
    }
}
