using System;
using System.Threading.Tasks;
using Yara.AppService.Dtos;

namespace Yara.AppService.Interfaces
{
    public interface IAppServiceCultura : IAppServiceBase<CulturaDto>
    {
        Task<bool> InsertAsync(CulturaDto obj);
        Task<bool> Inactive(Guid id);
    }
}
