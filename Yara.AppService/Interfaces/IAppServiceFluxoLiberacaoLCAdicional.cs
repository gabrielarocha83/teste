using System;
using System.Threading.Tasks;
using Yara.AppService.Dtos;

namespace Yara.AppService.Interfaces
{
    public interface IAppServiceFluxoLiberacaoLCAdicional : IAppServiceBase<FluxoLiberacaoLCAdicionalDto>
    {
        Task<bool> InsertAsync(FluxoLiberacaoLCAdicionalDto obj);
        Task<bool> Inactive(Guid id, Guid userIdAlteracao);
    }
}
