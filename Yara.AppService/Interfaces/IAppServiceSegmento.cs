using System;
using System.Threading.Tasks;
using Yara.AppService.Dtos;

namespace Yara.AppService.Interfaces
{
    public interface IAppServiceSegmento : IAppServiceBase<SegmentoDto>
    {
        Task<bool> Inactive(Guid id);
    }
}
