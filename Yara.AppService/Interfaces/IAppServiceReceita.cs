using System;
using System.Threading.Tasks;
using Yara.AppService.Dtos;

namespace Yara.AppService.Interfaces
{
    public interface IAppServiceReceita : IAppServiceBase<ReceitaDto>
    {
        Task<bool> InsertAsync(ReceitaDto obj);
        Task<bool> Inactive(Guid id);
    }
}
