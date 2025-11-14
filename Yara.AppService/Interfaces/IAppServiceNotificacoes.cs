using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Yara.AppService.Interfaces
{
    public interface IAppServiceNotificacoes
    {
        Task<IEnumerable<KeyValuePair<bool, Guid>>> NotificacoesCockpitUsuarios(string empresa, string urlCockpit, string urlContaClient);
    }
}
