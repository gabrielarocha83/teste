using System;
using System.Threading.Tasks;

namespace Yara.AppService.Interfaces
{
    public interface IAppServiceProposta
    {
        Task<string> ExistePropostaEmAndamentoAsync(Guid contaClienteID, string empresaID);
    }
}