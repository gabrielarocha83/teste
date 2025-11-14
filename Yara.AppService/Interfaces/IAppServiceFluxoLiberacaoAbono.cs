using System;
using System.Threading.Tasks;
using Yara.AppService.Dtos;

namespace Yara.AppService.Interfaces
{
    public interface IAppServiceFluxoLiberacaoAbono : IAppServiceBase<FluxoLiberacaoAbonoDto>
    {
        Task<bool> InsertAsync(FluxoLiberacaoAbonoDto obj);
        Task<bool> Inactive(Guid id, Guid userIdAlteracao);
    }
}
