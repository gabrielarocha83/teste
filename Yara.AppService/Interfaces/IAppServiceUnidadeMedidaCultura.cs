using System;
using System.Threading.Tasks;
using Yara.AppService.Dtos;

namespace Yara.AppService.Interfaces
{
    public interface IAppServiceUnidadeMedidaCultura : IAppServiceBase<UnidadeMedidaCulturaDto>
    {
        Task<bool> InsertAsync(UnidadeMedidaCulturaDto obj);
    }
}