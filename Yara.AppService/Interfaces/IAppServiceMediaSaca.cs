using System;
using System.Threading.Tasks;
using Yara.AppService.Dtos;

namespace Yara.AppService.Interfaces
{
    public interface IAppServiceMediaSaca : IAppServiceBase<MediaSacaDto>
    {
        Task<bool> InsertAsync(MediaSacaDto obj);
        Task<bool> Inactive(Guid id);
    }
}
