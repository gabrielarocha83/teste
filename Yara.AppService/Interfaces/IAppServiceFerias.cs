using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yara.AppService.Dtos;
using Yara.Domain.Entities;

namespace Yara.AppService.Interfaces
{
    public interface IAppServiceFerias : IAppServiceBase<FeriasDto>
    {
        Task<bool> InsertAsync(FeriasDto obj);
        Task<bool> Inactive(Guid id);
        Task<IEnumerable<FeriasDto>> GetFeriasByIDUser(Guid user);
    }
}
