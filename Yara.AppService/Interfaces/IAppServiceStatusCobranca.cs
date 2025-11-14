using System;
using System.Threading.Tasks;
using Yara.AppService.Dtos;

namespace Yara.AppService.Interfaces
{
    public interface IAppServiceStatusCobranca : IAppServiceBase<StatusCobrancaDto>
    {
        Task<bool> Inactive(Guid id);
        //Task<bool> InsertAsync(StatusCobrancaDto obj);
    }
}