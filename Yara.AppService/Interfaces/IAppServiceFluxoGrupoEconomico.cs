using System;
using System.Threading.Tasks;
using Yara.AppService.Dtos;

namespace Yara.AppService.Interfaces
{
    public interface IAppServiceFluxoGrupoEconomico : IAppServiceBase<FluxoGrupoEconomicoDto>
    {
        Task<bool> InsertAsync(FluxoGrupoEconomicoDto obj);
        Task<bool> InactiveAsync(Guid id, Guid userIdAlteracao);
    }
}
