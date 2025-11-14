using System;
using System.Threading.Tasks;
using Yara.AppService.Dtos;

namespace Yara.AppService.Interfaces
{
    public interface IAppServiceProdutividadeMedia : IAppServiceBase<ProdutividadeMediaDto>
    {
        Task<bool> InsertAsync(ProdutividadeMediaDto obj);
        Task<bool> Inactive(Guid id);
    }
}
