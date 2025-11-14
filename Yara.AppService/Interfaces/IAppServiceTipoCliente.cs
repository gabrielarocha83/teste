using System;
using System.Threading.Tasks;
using Yara.AppService.Dtos;

namespace Yara.AppService.Interfaces
{
    public interface IAppServiceTipoCliente:IAppServiceBase<TipoClienteDto>
    {
        Task<bool> Inactive(Guid id);
    }
}