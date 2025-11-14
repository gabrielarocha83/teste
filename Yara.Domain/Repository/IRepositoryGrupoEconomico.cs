using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yara.Domain.Entities;
using Yara.Domain.Entities.Procedures;

namespace Yara.Domain.Repository
{
    public interface IRepositoryGrupoEconomico : IRepositoryBase<GrupoEconomico>
    {
        Task<IEnumerable<BuscaGrupoEconomico>> BuscaGrupoEconomico(Guid codCliente, string empresaId);
        Task<IEnumerable<BuscaGrupoEconomico>> BuscaGrupoEconomicoPorGrupo(Guid codGrupo, string empresaId);
        Task<IEnumerable<BuscaHistoricoGrupo>> BuscaHistoricoPorGrupo(Guid codGrupo, string empresaId);
    }
}
