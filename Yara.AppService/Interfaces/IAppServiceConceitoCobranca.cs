using System;
using System.Threading.Tasks;
using Yara.AppService.Dtos;

namespace Yara.AppService.Interfaces
{
    public interface IAppServiceConceitoCobranca : IAppServiceBase<ConceitoCobrancaDto>
    {
        Task<bool> Inactive(Guid id);
        Task<bool> InsertAsync(ConceitoCobrancaDto obj);
    }
}