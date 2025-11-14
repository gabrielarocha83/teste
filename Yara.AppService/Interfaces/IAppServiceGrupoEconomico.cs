using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yara.AppService.Dtos;

namespace Yara.AppService.Interfaces
{
    public interface IAppServiceGrupoEconomico : IAppServiceBase<GrupoEconomicoDto>
    {
        Task<bool> InsertAsync(NovoGrupoEconomicoDto obj);
        Task<bool> DeleteAsync(GrupoEconomicoFluxoDto obj, string URL);
        Task<IEnumerable<BuscaGrupoEconomicoDto>> BuscaGrupoEconomico(Guid clienteId, string empresaId);
        Task<IEnumerable<BuscaGrupoEconomicoDto>> BuscaGrupoEconomicoPorGrupo(Guid grupoId, string empresaId);
        Task<IEnumerable<BuscaHistoricoGrupoDto>> BuscaHistoricoPorGrupo(Guid codGrupo, string empresaId);
    }
}