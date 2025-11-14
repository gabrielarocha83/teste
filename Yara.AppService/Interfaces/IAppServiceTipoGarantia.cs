using System;
using System.Threading.Tasks;
using Yara.AppService.Dtos;

namespace Yara.AppService.Interfaces
{
    public interface IAppServiceTipoGarantia : IAppServiceBase<TipoGarantiaDto>
    {
        Task<bool> InsertAsync(TipoClienteDto obj);
    }
}