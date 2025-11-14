using System;
using System.Threading.Tasks;
using Yara.AppService.Dtos;

namespace Yara.AppService.Interfaces
{
    public interface IAppServiceAreaIrrigada : IAppServiceBase<AreaIrrigadaDto>
    {
        Task<bool> InsertAsync(AreaIrrigadaDto obj);
        Task<bool> Inactive(Guid id);
    }
}
