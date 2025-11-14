using System.Threading.Tasks;
using Yara.AppService.Dtos;

namespace Yara.AppService.Interfaces
{
    public interface IAppServiceTipoEndividamento : IAppServiceBase<TipoEndividamentoDto>
    {
        Task<bool> InsertAsync(TipoEndividamentoDto obj);
    }
}
