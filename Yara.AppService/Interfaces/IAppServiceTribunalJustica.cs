using System;
using System.Threading.Tasks;
using Yara.AppService.Dtos;

namespace Yara.AppService.Interfaces
{
    public interface IAppServiceTribunalJustica : IAppServiceBase<TribunalJusticaDto>
    {
        Task<bool> CheckAsync(Guid contaClienteID);
        Task<bool> InsertAsync(TribunalJusticaDto obj);
    }
}