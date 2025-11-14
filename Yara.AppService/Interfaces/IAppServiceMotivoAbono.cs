using System;
using System.Threading.Tasks;
using Yara.AppService.Dtos;

namespace Yara.AppService.Interfaces
{
    public interface IAppServiceMotivoAbono : IAppServiceBase<MotivoAbonoDto>
    {
        Task<bool> Inactive(Guid id);
        Task<bool> InsertAsync(MotivoAbonoDto obj);
    }
}