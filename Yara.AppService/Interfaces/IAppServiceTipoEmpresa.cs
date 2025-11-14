using System;
using System.Threading.Tasks;
using Yara.AppService.Dtos;

namespace Yara.AppService.Interfaces
{
    public interface IAppServiceTipoEmpresa : IAppServiceBase<TipoEmpresaDto>
    {
        Task<bool> InsertAsync(TipoEmpresaDto obj);
        Task<bool> Inactive(Guid id);
    }
}
