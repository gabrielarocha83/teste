using System.Threading.Tasks;
using Yara.AppService.Dtos;

namespace Yara.AppService.Interfaces
{
    public interface IAppServicePorcentagemQuebra : IAppServiceBase<PorcentagemQuebraDto>
    {
        Task<bool> InsertAsync(PorcentagemQuebraDto obj);
    }
}