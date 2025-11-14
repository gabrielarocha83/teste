using System.Collections.Generic;
using System.Threading.Tasks;
using Yara.AppService.Dtos;

namespace Yara.AppService.Interfaces
{
    public interface IAppServiceTitulo : IAppServiceBase<TituloDto>
    {
        Task<bool> InsertAsync(TituloDto obj);
        Task<bool> UpdateList(IEnumerable<TituloDto> obj);
        Task<bool> UpdateStatus(TituloAtualizacaoStatus obj, string empresa);
    }
}
