using System;
using System.Threading.Tasks;
using Yara.AppService.Dtos;

namespace Yara.AppService.Interfaces
{
    public interface IAppServiceCulturaEstado : IAppServiceBase<CulturaEstadoDto>
    {
        Task<bool> InsertAsync(CulturaEstadoDto obj);
    }
}
