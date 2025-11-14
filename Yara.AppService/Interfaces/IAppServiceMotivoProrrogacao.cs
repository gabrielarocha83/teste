using System;
using System.Threading.Tasks;
using Yara.AppService.Dtos;

namespace Yara.AppService.Interfaces
{
    public interface IAppServiceMotivoProrrogacao : IAppServiceBase<MotivoProrrogacaoDto>
    {
        Task<bool> Inactive(Guid id);
        Task<bool> InsertAsync(MotivoProrrogacaoDto obj);
    }
}