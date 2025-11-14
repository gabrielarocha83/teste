using System.Threading.Tasks;
using Yara.AppService.Dtos;

namespace Yara.AppService.Interfaces
{
    public interface IAppServiceIdadeMediaCanavial : IAppServiceBase<IdadeMediaCanavialDto>
    {
        Task<bool> InsertAsync(IdadeMediaCanavialDto obj);
    }
}
