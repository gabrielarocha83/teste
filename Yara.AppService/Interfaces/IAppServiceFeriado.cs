using System;
using System.Threading.Tasks;
using Yara.AppService.Dtos;

namespace Yara.AppService.Interfaces
{
    public interface IAppServiceFeriado : IAppServiceBase<FeriadoDto>
    {
        Task<bool> InsertAsync(FeriadoDto obj);
        Task<bool> Inactive(Guid id);
    }
}
