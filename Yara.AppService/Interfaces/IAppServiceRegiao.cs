using System.Collections.Generic;
using System.Threading.Tasks;
using Yara.AppService.Dtos;
using Yara.Domain.Entities;

namespace Yara.AppService.Interfaces
{
    public interface IAppServiceRegiao
    {
        Task<IEnumerable<RegiaoDto>> GetAllRegion();
    }
}
