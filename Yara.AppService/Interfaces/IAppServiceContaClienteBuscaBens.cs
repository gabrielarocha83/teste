using System.Threading.Tasks;
using Yara.AppService.Dtos;

namespace Yara.AppService.Interfaces
{
    public interface IAppServiceContaClienteBuscaBens : IAppServiceBase<ContaClienteBuscaBensDto>
    {
        Task<bool> InsertAsync(ContaClienteBuscaBensDto obj);
    }
}
