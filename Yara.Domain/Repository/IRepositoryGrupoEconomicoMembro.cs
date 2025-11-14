using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yara.Domain.Entities;
using Yara.Domain.Entities.Procedures;

namespace Yara.Domain.Repository
{
    public interface IRepositoryGrupoEconomicoMembro: IRepositoryBase<GrupoEconomicoMembros>
    {
        Task<IEnumerable<BuscaGrupoEconomicoMembros>> BuscaContaCliente(Guid grupoId, string EmpresaID);
    }
}
