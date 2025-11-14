using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Yara.AppService.Dtos;

namespace Yara.AppService.Interfaces
{
    public interface IAppServiceContaClienteGarantia : IAppServiceBase<ContaClienteGarantiaDto>
    {
        Task<bool> Inactive(Guid id);
        Task<ContaClienteGarantiaDto> InsertAsync(ContaClienteGarantiaDto obj);
        Task<ContaClienteGarantiaDto> UpdateAsync(ContaClienteGarantiaDto obj);
        Task<IEnumerable<ContaClienteGarantiaDto>> GetAllFilterAsyncAll(string documento);
    }
}
