using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yara.Domain.Entities.Procedures;

namespace Yara.Domain.Repository.Procedures
{
    public interface IRepositoryBuscaGrupoCookpit
    {
        Task<IEnumerable<BuscaGrupoEconomico>> BuscaGrupoEconomico(Guid usuarioID, string EmpresaID);
    }
}