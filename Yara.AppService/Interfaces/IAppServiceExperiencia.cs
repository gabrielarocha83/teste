using System;
using System.Threading.Tasks;
using Yara.AppService.Dtos;

namespace Yara.AppService.Interfaces
{
    public interface IAppServiceExperiencia : IAppServiceBase<ExperienciaDto>
    {
        Task<bool> Inactive(Guid id);
        Task<bool> InsertAsync(ExperienciaDto obj);
    }
}
