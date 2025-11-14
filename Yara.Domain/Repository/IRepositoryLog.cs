using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yara.Domain.Entities;
using Yara.Domain.Entities.Procedures;

namespace Yara.Domain.Repository
{
    public interface IRepositoryLog : IRepositoryBase<Log>
    {
        Task<IEnumerable<LogRetorno>> BuscaLog(BuscaLogs busca);
        Task<IEnumerable<BuscaLogFluxoAutomatico>> BuscaLogFluxoAutomatico(BuscaLogFluxoAutomatico busca);
        Task<IEnumerable<LogwithUser>> BuscaLogGrupoEconomico(Guid ContaClienteID, string EmpresaID);
        Task<IEnumerable<LogwithUser>> BuscaLogProposta(Guid propostaId);
    }
}
