using System.Data.Entity;
using Yara.Domain.Entities;
using Yara.Domain.Repository;

namespace Yara.Data.Entity.Repository
{
    public class RepositoryFluxoGrupoEconomico : RepositoryBase<FluxoGrupoEconomico>, IRepositoryFluxoGrupoEconomico
    {
        public RepositoryFluxoGrupoEconomico(DbContext context) : base(context)
        {
        }
    }
}
