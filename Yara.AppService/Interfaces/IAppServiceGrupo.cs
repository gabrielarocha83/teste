using System;
using System.Threading.Tasks;
using Yara.AppService.Dtos;

namespace Yara.AppService.Interfaces
{
    public interface IAppServiceGrupo: IAppServiceBase<GrupoDto>
    {
        Task<bool> Inactive(Guid id);
        Task<bool> InsertAsync(GrupoDto obj);
    }
}