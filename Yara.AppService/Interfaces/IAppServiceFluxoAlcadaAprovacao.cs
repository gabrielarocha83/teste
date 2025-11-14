using System;
using System.Threading.Tasks;
using Yara.AppService.Dtos;

namespace Yara.AppService.Interfaces
{
    public interface IAppServiceFluxoAlcadaAprovacao : IAppServiceBase<FluxoAlcadaAprovacaoDto>
    {
        Task<bool> InsertAsync(FluxoAlcadaAprovacaoDto obj);
        Task<bool> Inactive(Guid id, Guid userIdAlteracao);
    }
}
