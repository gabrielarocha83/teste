using System.Data.Entity;
using Yara.Domain.Entities;
using Yara.Domain.Repository;

namespace Yara.Data.Entity.Repository
{
    public class RepositoryFluxoAlcadaAnalise : RepositoryBase<FluxoAlcadaAnalise>, IRepositoryFluxoAlcadaAnalise
    {
        public RepositoryFluxoAlcadaAnalise(DbContext context) : base(context)
        {
        }
    }
}
