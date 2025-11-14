using System;
using System.Threading.Tasks;
using Yara.AppService.Dtos;

namespace Yara.AppService.Interfaces
{
    public interface IAppServiceTipoPecuaria : IAppServiceBase<TipoPecuariaDto>
    {
        Task<bool> Inactive(Guid id);
    }
}