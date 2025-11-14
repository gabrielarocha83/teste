using System.Threading.Tasks;
using Yara.AppService.Dtos;

namespace Yara.AppService.Interfaces
{
    public interface IAppServiceContaClienteVisita : IAppServiceBase<ContaClienteVisitaDto>
    {
        Task<bool> InsertAsync(ContaClienteVisitaDto obj);
    }
}
