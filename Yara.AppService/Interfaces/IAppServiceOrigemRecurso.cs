using System;
using System.Threading.Tasks;
using Yara.AppService.Dtos;

namespace Yara.AppService.Interfaces
{
    public interface IAppServiceOrigemRecurso : IAppServiceBase<OrigemRecursoDto>
    {
        Task<bool> Inactive(Guid id);
        Task<bool> InsertAsync(OrigemRecursoDto obj);
    }
}