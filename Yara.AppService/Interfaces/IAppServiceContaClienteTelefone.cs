using System.Collections.Generic;
using System.Threading.Tasks;
using Yara.AppService.Dtos;

namespace Yara.AppService.Interfaces
{
    public interface IAppServiceContaClienteTelefone : IAppServiceBase<ContaClienteTelefoneDto>
    {
        Task<bool> Inactive(ContaClienteTelefoneDto obj);
        Task<bool> InsertOrUpdateManyAsync(IEnumerable<ContaClienteTelefoneDto> obj);
    }
}