using System;
using System.Threading.Tasks;
using Yara.AppService.Dtos;

namespace Yara.AppService.Interfaces
{
    public interface IAppServiceProdutoServico : IAppServiceBase<ProdutoServicoDto>
    {
        Task<bool> InsertAsync(ProdutoServicoDto obj);
        Task<bool> Inactive(Guid id);
    }
}
