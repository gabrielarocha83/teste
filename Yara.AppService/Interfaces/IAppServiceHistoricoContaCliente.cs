using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yara.AppService.Dtos;

namespace Yara.AppService.Interfaces
{
    public interface IAppServiceHistoricoContaCliente
    {
        Task<IEnumerable<HistoricoContaClienteDto>> GetAllHistoryAccountClient(Guid clientID, string company);
    }
}
