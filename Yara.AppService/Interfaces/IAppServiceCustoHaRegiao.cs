using System.Threading.Tasks;
using Yara.AppService.Dtos;

namespace Yara.AppService.Interfaces
{
    public interface IAppServiceCustoHaRegiao : IAppServiceBase<CustoHaRegiaoDto>
    {
        Task<bool> InsertAsync(CustoHaRegiaoDto obj);
       
    }
}
