using System;
using System.Threading.Tasks;
using Yara.AppService.Dtos;

namespace Yara.AppService.Interfaces
{
    public interface IAppServiceFluxoLiberacaoLimiteCredito : IAppServiceBase<FluxoLiberacaoLimiteCreditoDto>
    {
        Task<bool> InsertAsync(FluxoLiberacaoLimiteCreditoDto obj);
        Task<bool> Inactive(Guid id, Guid userIdAlteracao);
    }
}
