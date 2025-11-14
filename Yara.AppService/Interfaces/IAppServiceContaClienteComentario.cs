using System;
using System.Threading.Tasks;
using Yara.AppService.Dtos;

namespace Yara.AppService.Interfaces
{
    public interface IAppServiceContaClienteComentario : IAppServiceBase<ContaClienteComentarioDto>
    {
        Task<bool> Inactive(Guid id);
    }
}
