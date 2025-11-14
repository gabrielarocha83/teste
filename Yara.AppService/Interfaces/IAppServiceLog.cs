using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Yara.AppService.Dtos;

namespace Yara.AppService.Interfaces
{
    public interface IAppServiceLog
    {
        bool Create(LogDto log);
        Task<IEnumerable<LogDto>> GetAllFilter<TKey>(Expression<Func<LogDto, bool>> expression);
        Task<IEnumerable<LogDto>> GetAllAsync();
        Task<IEnumerable<LogDto>> BuscaLog(BuscaLogsDto busca);
        Task<IEnumerable<LogContaClienteDto>> BuscaLogContaCliente(BuscaLogsDto busca);
        Task<IEnumerable<BuscaLogFluxoAutomaticoDto>> BuscaLogFluxoAutomatico(BuscaLogFluxoAutomaticoDto busca);
        Task<IEnumerable<LogWithUserDto>> BuscaLogGrupoEconomico(Guid ContaClienteID, string EmpresaID);
        Task<IEnumerable<LogWithUserDto>> BuscaLogProposta(Guid propostaId);
    }
}